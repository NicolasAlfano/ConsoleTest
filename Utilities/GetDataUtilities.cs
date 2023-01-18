using ConsoleTest.Models;
using ConsoleTest.Services;

namespace ConsoleTest.Utilities
{


    public class GetDataUtilities : GetDataServices, MapperServices, SearchStudentsServices
    {
        private int NumberCases { get; set; }
        private List<Student> Cases { get; set; } = new();
        private SearchCase SearchCase { get; set; }
        private readonly TxtReaderUtilities txtReader;

        public GetDataUtilities(TxtReaderUtilities txtReader)
        {
            this.txtReader = txtReader;
            GetData();
        }

        public void GetData()
        {
            Console.WriteLine("Insert Number of Cases: ");
            NumberCases = int.Parse(Console.ReadLine());

            for (int i = 1; i <= NumberCases; i++)
            {
                //TO DO: Validacion
                Console.WriteLine("Insert Case Number " + i + " following the format (gender,age,studies,academicYear: ");
                string[] items = Console.ReadLine().Split(',');
                Mapper(items);
                var results = GetStudents();
                var busqueda = "";

                for (int j = 0; j < results.Count(); j++)
                {
                     busqueda = string.Concat(busqueda, results[j].ToString());
                     if((j+1) != results.Count()){
                        busqueda = string.Concat(busqueda, ",");
                     }
                }
            
                Console.WriteLine("Result Nº "+i + ":");
                Console.WriteLine(busqueda);
            }
        }
        public void Mapper(string[] items)
        {
            SearchCase = new SearchCase
            {
                Gender = items[0],
                Age = items[1],
                Studies = items[2],
                AcademicYear = items[3]
            };
        }

        public List<string> GetStudents()
        {
            var results = SearchInDictionary();

            if (results.Count() == 0)
            {
                results.Add("NONE");
            }

            results.Sort();
            return results;
        }

        private new List<string> SearchInDictionary()
        {
            var result = new List<string>();
            var auxPosiciones = new List<int>();
            var auxPosicionesNuevas = new List<int>();
            var students = txtReader.GetStudents();

            var genderDictionary = txtReader.GetGenderDictionary();
            var yearDictionary = txtReader.GetYearDictionary();
            var studyDictionary = txtReader.GetStudyDictionary();
            var academicYearDictionary = txtReader.GetAcademicYearDictionary();

            var genders = genderDictionary[SearchCase.Gender.ToString()];
            var years = yearDictionary[SearchCase.Age.ToString()];
            var studies = studyDictionary[SearchCase.Studies.ToString()];
            var academicYears = academicYearDictionary[SearchCase.AcademicYear.ToString()];

            try
            {
                if (genders.Count() != 0 && years.Count() != 0  && studies.Count() != 0  && academicYears.Count() != 0)
                {   
                    for (int i = 0; i < genders.Count(); i++)
                    {
                        for (int j = 0; j < years.Count(); j++)
                        {
                            if (genders[i] == years[j])
                            {
                                auxPosiciones.Add(years[j]);
                            }
                        }
                    }

                    if (auxPosiciones.Count() != 0)
                    {
                        for (int j = 0; j < auxPosiciones.Count(); j++)
                        {
                            for (int i = 0; i < studies.Count(); i++)
                            {
                                if (auxPosiciones[j] == studies[i])
                                {
                                    auxPosicionesNuevas.Add(auxPosiciones[j]);
                                }
                            }
                        }
                    }

                    if (auxPosicionesNuevas.Count() != 0)
                    {
                        for (int j = 0; j < auxPosicionesNuevas.Count(); j++)
                        {
                            for (int i = 0; i < academicYears.Count(); i++)
                            {
                                if (auxPosicionesNuevas[j] == academicYears[i])
                                {
                                    // auxPosicionesNuevas.Add(academicYears[i]);
                                    var posicion = auxPosicionesNuevas[j];

                                    var name = students[posicion].Name;
                                    result.Add(name);
                                }
                            }
                        }
                    }

                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            return result;
        }

    }
}