using ConsoleTest.Models;
using ConsoleTest.Services;

namespace ConsoleTest.Utilities;

public class TxtReaderUtilities : TxtReaderServices, MapperServices
{

    private List<Student> Students { get; set; } = new();
    private Dictionary<string, List<int>> GenderDictionary { get; set; } = new();
    private Dictionary<string, List<int>> YearDictionary { get; set; } = new();
    private Dictionary<string, List<int>> StudyDictionary { get; set; } = new();
    private Dictionary<string, List<int>> AcademicYearDictionary { get; set; } = new();
    private int Position { get; set; } = 0;

    public TxtReaderUtilities(string path)
    {
        Read(path);
    }

    private void Mapper(string line)
    {
        string[] items = line.Split(',');
        Mapper(items);

        DictionaryHandling(GenderDictionary, items[1]);
        DictionaryHandling(YearDictionary, items[2]);
        DictionaryHandling(StudyDictionary, items[3]);
        DictionaryHandling(AcademicYearDictionary, items[4]);

    }
    public void Mapper(string[] items)
    {
        Students.Add(new Student
        {
            Name = items[0],
            Gender = items[1],
            Age = items[2],
            Studies = items[3],
            AcademicYear = items[4]
        });
    }
    private void DictionaryHandling(Dictionary<String, List<int>> diccionario, string item)
    {

        var positions = new List<int>();
        var valor = diccionario.TryGetValue(item, out positions);

        if (!valor)
        {
            var posicionesAux = new List<int>();
            posicionesAux.Add(Position);
            diccionario.Add(item, posicionesAux);
        }
        else
        {
            diccionario[item].Add(Position);
        }
    }
    public void Read(string path)
    {
        String line;

        StreamReader streamReader = new StreamReader(path);

        line = streamReader.ReadLine();

        while (line != null)
        {
            Mapper(line);

            Console.WriteLine("N: " + Position);
            Position++;

            line = streamReader.ReadLine();
        }
        streamReader.Close();
    }
    public Dictionary<string, List<int>> GetGenderDictionary()
    {
        return GenderDictionary;
    }

    public Dictionary<string, List<int>> GetYearDictionary()
    {
        return YearDictionary;
    }

    public Dictionary<string, List<int>> GetStudyDictionary()
    {
        return StudyDictionary;
    }

    public Dictionary<string, List<int>> GetAcademicYearDictionary()
    {
        return AcademicYearDictionary;
    }

    public List<Student> GetStudents()
    {
        return Students;
    }

}