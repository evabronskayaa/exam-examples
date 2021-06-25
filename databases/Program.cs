using System;
using System.Collections.Generic;
using System.IO;

namespace databases
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите название таблицы и кол-во столбцов");
            AddTableValues(Console.ReadLine(), int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine()));
            
            Console.WriteLine("На какую таблицу первая ссылается? Укажите название таблицы и кол-во столбцов");
            AddTableValues(Console.ReadLine(), int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine()));
        }

        private static void AddTableValues(string tableName, int columnsCount, int rowsCount) {
            Column[] columns = new Column[columnsCount];
            for (int i = 0; i < columnsCount; i++) {
                Console.WriteLine("Введите название столбца и тип данных:");
                string[] text = Console.ReadLine().Split(';');
                
                DataType type = GetType(text[1]);
                Column column = new Column(text[0], type);
                columns[i] = column;
            }

            Table table = new Table(columns);
            for(int i = 0; i < rowsCount; i++) {
                Console.WriteLine("Введите значения:");
                string[] text = Console.ReadLine().Split(';');
                
                object[] values = new object[columnsCount];
                for (int j = 0; j < text.Length; j++) {
                    values[j] = Serialize(columns[j].Type, text[j]);
                }

                Row row = new Row(values);
                table.TableRows.Add(row);
            }
            WriteTables(tableName, table);
        }

        private static void WriteTables(string tableName, Table table) {
            File.AppendAllText("tables.txt", tableName + "\n");
            string line = String.Empty;
            for (int i = 0; i < tableName.Length; i++) {
                line += "-";
            }
            File.AppendAllText("tables.txt",  line + "\n");

            GetTypeTable(table);
            GetValueTable(table);
            File.AppendAllText("tables.txt","\n");
        }
        private static void GetTypeTable(Table table) {
            foreach (var column in table.Columns) {
                File.AppendAllText("tables.txt",column.Name + ";" +column.Type + ";");
                File.AppendAllText("tables.txt","\n");
            }
        }

        private static void GetValueTable(Table table) {
            foreach (var column in table.TableRows) {
                foreach (var row in column.TableRow) {
                    File.AppendAllText("tables.txt",row + ";");
                }
                File.AppendAllText("tables.txt","\n");
            }
        }

        private static DataType GetType(string text) {
            if (text == "INT") {
                return DataType.INT;
            } else if (text == "STRING") {
                return DataType.STRING;
            } else {
                return DataType.BOOLEAN;
            }
        }

        private static object Serialize(DataType type, string s) {
            if (type == DataType.INT) {
                return int.Parse(s);
            } else if (type == DataType.BOOLEAN) {
                return bool.Parse(s);
            } else {
                return s;
            }
        }
    }

    class Table
    {
        public List<Row> TableRows { get; }
        public Column[] Columns { get; }
        public Table(Column[] columns) {
            TableRows = new List<Row>();
            Columns = columns;
        }
    }

    class Row
    {
        public object[] TableRow { get; }
        public Row(object[] values) {
            TableRow = values;
        }
    }

    class Column
    {
        public string Name { get; }
        public DataType Type { get; }
        public Column(string name, DataType type) {
            Name = name;
            Type = type;
        }
    }

    enum DataType {
        INT,
        STRING,
        BOOLEAN
    }
}