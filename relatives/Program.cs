using System;
using System.Collections.Generic;
using System.IO;

namespace relatives
{
    class Program
    {
        private static Dictionary<int, Person> people = new Dictionary<int, Person>();
        static void Main(string[] args)
        {
            string[] file = File.ReadAllLines("data.txt");
            List<string> pattern = new List<string>(file[0].Split(';'));

            for (int i = 1; i < file.Length; i++) 
            {
                if (!file[i].Equals("") && !file[i].Contains("<->")) 
                {
                    string[] parts = file[i].Split(';');
                    int id = int.Parse(parts[pattern.IndexOf("Id")]);
                    DateTime birthDate = DateTime.Parse(parts[pattern.IndexOf("BirthDate")]);
                    people.Add(id, new Person(id, parts[pattern.IndexOf("LastName")], parts[pattern.IndexOf("FirstName")], birthDate));
                }

                if (file[i].Contains("<->")) 
                {
                    FillPersonRelationship(file[i]);
                }
            }
        }
        
        private static void FillPersonRelationship(string personRelationInfo) {
            personRelationInfo = personRelationInfo.Replace("<->", " ").Replace('=', ' ');
            string[] parts = personRelationInfo.Split(' ');

            Relationship relationship = new Relationship(people[int.Parse(parts[0])], people[int.Parse(parts[1])], GetRelationType(parts[2]));
            relationship.Person1.Relations.Add(relationship);
            relationship.Person2.Relations.Add(relationship);
        }

        private static RelationshipType GetRelationType(string relation) {
            return relation == "spouse"
                ? RelationshipType.SPOUSE
                : relation == "sibling"
                    ? RelationshipType.SIBLING
                    : RelationshipType.PARENT;
        }
    }

    class Person
    {
        public int Id { get; }
        public string LastName { get; }
        public string FirstName { get; }
        public DateTime BirthDate { get; }
        public List<Relationship> Relations { get; }

        public Person(int id, string lastName, string firstName, DateTime birthDate) {
            Id = id;
            LastName = lastName;
            FirstName = firstName;
            BirthDate = birthDate;
            Relations = new List<Relationship>();
        }
    }
    
    class Relationship
    {
        public Person Person1 { get; }
        public Person Person2{ get; }
        public RelationshipType RelationType{ get; }
        
        public Relationship(Person person1, Person person2, RelationshipType type) {
            Person1 = person1;
            Person2 = person2;
            RelationType = type;
        }
    }

    enum RelationshipType {
        SPOUSE,
        PARENT,
        SIBLING
    }
}