namespace ArgoPantry.WPF.Services; 
public class CSVParserService {
    public void ParseAndDetermineType(string filePath) {
        var lines = File.ReadAllLines(filePath);
        if(lines.Length == 0) return;

        var headers = lines[0].Split(',');

        if(IsStudentHeaders(headers)) {
            var students = ParseStudents(lines.Skip(1));
        }
        else if(IsItemHeaders(headers)) {
            var items = ParseItems(lines.Skip(1));
        }
    }

    private bool IsStudentHeaders(string[] headers) {
        return headers.Contains("First Name") &&
               headers.Contains("Last Name") &&
               headers.Contains("Student ID");
    }

    private bool IsItemHeaders(string[] headers) {
        return headers.Contains("ItemName") &&
               headers.Contains("Barcode") &&
               headers.Contains("Quantity");
    }

    private IEnumerable<Student> ParseStudents(IEnumerable<string> lines) {
        List<Student> students = new List<Student>();

        foreach(var line in lines) {
            var fields = line.Split(',');

            if(fields.Length >= 3) {
                students.Add(new Student {
                    FirstName = fields[0],
                    LastName = fields[1],
                    StudentId = fields[2]
                });
            }
        }

        return students;
    }

    private IEnumerable<Item> ParseItems(IEnumerable<string> lines) {
        List<Item> items = new List<Item>();

        foreach(var line in lines) {
            var fields = line.Split(',');

            if(fields.Length >= 3) {
                items.Add(new Item {
                    ItemName = fields[0],
                    Barcode = fields[1],
                    Quantity = int.Parse(fields[2])
                });
            }
        }

        return items;
    }
}