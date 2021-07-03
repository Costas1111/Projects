using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Solution.Namespace  
{
    class Program
    {
        static void Main(string[] args)
        {
            Exec Class = new Exec();
            Class.Init();
        }
    }


    /// <summary>
    /// Exec class.
    /// </summary>
    class Exec
    {
        /// <summary>
        /// The entry point for the the class.
        /// </summary>
        public void Init() 
        {
            
            
                for (int i = 0; i < _files.Length; i++) 
                {
                    Console.WriteLine((i + 1) + ". " + _files[i]);
                }
                Console.WriteLine("Choose one of the above files by typing the corresponding number next to them : ");
                string FileNumber = Console.ReadLine();

                Console.WriteLine(_files[int.Parse(FileNumber) - 1]);
                FileOpen("datasets/" + _files[int.Parse(FileNumber) - 1]);
                _lessons.Sort();
                var lessonsCount = _lessons.Count; 
                _examStudents = new List<List<int>>(lessonsCount);        
                for (int i = 0; i < lessonsCount; i++)
                {
                    _examStudents.Add(new List<int>());
                }
            
                  
                AddExamStudents();
                _conflictsCount = 0;
                _conflictsUniqueCount = 0;
                Conflict_Lines_Arg = new List<int>(new int[lessonsCount]);
                CreateM_FindC();

            


                double density = _conflictsUniqueCount / ((double)(lessonsCount * lessonsCount));
                Console.WriteLine("Lessons : " + lessonsCount);
                Console.WriteLine("Students : " + _students);
                Console.WriteLine("Registrations : " + _registrations);
                Console.WriteLine("All Conficts : " + _conflictsCount);
                Console.WriteLine("Density : " + density);
                Console.WriteLine("Max : " + FindMax());
                Console.WriteLine("Min : " + FindMin());
                Console.WriteLine("Mean : " + FindMean());
                Console.WriteLine("Med : " + FindMed());
                Console.WriteLine("CV : " + FindCV());
                Console.WriteLine("Unique Colors Used : " + FirstFit());

        }

        private int FindMax()
        {
            var lessonsCount = _lessons.Count;
            int maxCol = 0;
            for (int i = 0; i < lessonsCount; i++)
            {
                int tempMax = 0;
                for (int j = 0; j < lessonsCount; j++)
                {
                    if (_lessonMatrix[i, j] > 0)
                        tempMax++;
                }
                maxCol = Math.Max(tempMax, maxCol); 
            }
            return maxCol;
        }

        private int FindMin()
        {
            var lessonsCount = _lessons.Count;
            int minCol = lessonsCount+1;
            for (int i = 0; i < lessonsCount; i++)
            {
                int tempMin = 0;
                for (int j = 0; j < lessonsCount; j++)
                {
                    if (_lessonMatrix[i, j] > 0)
                        tempMin++;
                }
                minCol = Math.Min(tempMin, minCol); 
            }
            return minCol;
        }


        public double FindMean()
        {
    return (double)((double)_conflictsUniqueCount/(double)_lessons.Count);
        }

        private double FindMed()
        {
            var lessonsCount = _lessons.Count;
            List<int> sorted = new List<int>();
            for (int i = 0; i < lessonsCount; i++)
            {
                int c = 0;
                for (int j = 0; j < lessonsCount; j++)
                {
                    if (_lessonMatrix[i, j] > 0)
                        c++;
                }
                sorted.Add(c);
            }
            sorted.Sort();
            return sorted[sorted.Count / 2 + (sorted.Count % 2 == 0 ? 0 : 1)];
        }

        private double FindCV()
        {
            var lessonsCount = _lessons.Count;
            for (int i = 0; i < lessonsCount; i++)
            {
                int c = 0;
                for (int j = 0; j < lessonsCount; j++)
                {
                    if (_lessonMatrix[i, j] > 0)
                        c++;
                }
                _calc += Math.Pow((c - FindMean()), 2); 
            }

            _calc /= lessonsCount; 
            return (Math.Sqrt(_calc) / FindMean()) * 100;

        }

        /*Δημιουργεί έναν δυσδιαστατο πινακα με το μηκος των μαθηματων όπου πχ.
        _lessonMatrix[0,5] περιέχει τα colisions του μαθήματος 0 με το μάθημα 5 */ 
        private void CreateM_FindC()
        {
            var lessonsCount = _lessons.Count;
            _lessonMatrix = new int[lessonsCount, lessonsCount];
            for (int i = 0; i < lessonsCount; i++)
            {
                for (int j = 0; j < lessonsCount; j++)
                {
                    if (i == j)
                    {
                        _lessonMatrix[i, j] = 0;   
                        continue;
                    }
                    _lessonMatrix[i, j] = FindColisions(_examStudents[i], _examStudents[j]);
                    if (_lessonMatrix[i, j] > 0)
                    {
                        
                        Conflict_Lines_Arg[i] += _lessonMatrix[i, j];
                        _conflictsCount += _lessonMatrix[i, j];
                        _conflictsUniqueCount++;
                    
                    }
                }
            }
        }
        /* Συγκρίνει με τη σειρά όλους τους μαθητές της πρώτης λιστας με όλους της δεύτερης λιστας ,
         εαν ο μαθητής υπάρχει και στις 2 λίστες τότε έχουμε 1 collision */
        private static int FindColisions(List<int> ls1, List<int> ls2) 
        {
            int col = 0;
            for (int i = 0; i < ls1.Count; i++)
            {
                for (int j = 0; j < ls2.Count; j++)
                {
                    if (ls1[i] == ls2[j])
                    {
                        col++;
                        break;
                    }
                }
            }
            return col;
        }

        /*Function που διαβάζει τις γραμμές του αρχείου*/
        private bool FileOpen(string path)
        {
            if (!File.Exists(path)) 
            {
                Console.WriteLine("File does not exist in given path! Given path: {path}");
                return false;
            }

            StreamReader sr = new StreamReader(path);
            var line = sr.ReadLine();
            while (!string.IsNullOrWhiteSpace(line))
            {
                _students++;
                string[] frag = line.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries);
                List<int> temp = new List<int>();
                for (int i = 0; i < frag.Length; i++)
                {
                    temp.Add(int.Parse(frag[i])); 
                    UniqueLessons(frag[i]);    
                }
                _studentExams.Add(temp);
                line = sr.ReadLine();
                
            }
                  
            return true;
        }
        
        /*Προσθέτει τους μαθητές στο μάθημα που έχουν δηλώσει*/
        private void AddExamStudents()
        {
            
            for (int student = 0; student < _studentExams.Count; student++)
            {
                for (int j = 0; j < _studentExams[student].Count; j++)
                {
                    
                    _registrations++;
                    _examStudents[_studentExams[student][j] - 1].Add(student);  

                }
                
            }

        }

        /*Προσθέτει τα μαθήματα με μοναδικό ID στη λίστα _lessons*/
        private void UniqueLessons(string lessonId)
        {
            if (_lessons.Contains(int.Parse(lessonId)))
                return;
            _lessons.Add(int.Parse(lessonId));
        }
        
        /*Βρίσκει αν υπάρχει κοινός φοιτητής ανάμεσα σε 2 μαθήματα , δηλαδή έχουμε σύνδεση κορυφών */
        private bool HasConnection(int lesson1, int lesson2)
        {
            for (int i = 0; i < _examStudents[lesson1].Count; i++)
            {
                for (int j = 0; j < _examStudents[lesson2].Count; j++)
                {
                    if (_examStudents[lesson1][i] == _examStudents[lesson2][j])
                        return true;
                }
            }
            return false;
        }

        /*Greedy αλγόριθμος χρωματισμού κορυφών γραφήματος*/
        private int FirstFit()
        {
            var lessonsCount = _lessons.Count;
            int max_colors = 0;
            int[] Lesson_Color = new int[lessonsCount];

            for (int i = 0; i < lessonsCount; i++)
            {
                int curColor = 1;
                bool colorFound = false;
                while (colorFound == false)
                {
                    colorFound = true;
                    for (int j = 0; j < lessonsCount; j++)
                    {
                        if (i != j && HasConnection(i, j))
                        {
                            if (curColor == Lesson_Color[j])
                            {
                                curColor++;
                                colorFound = false;
                                break;
                            }
                        }
                    }

                }
                if (curColor > max_colors)
                    max_colors = curColor;
                Lesson_Color[i] = curColor;

            }  
            return max_colors;

        }

        private List<List<int>> _examStudents;
        private int _conflictsCount;
        private int _conflictsUniqueCount;
        private List<int> Conflict_Lines_Arg;

        private int[,] _lessonMatrix;
        private double _calc = 0;
        private int _registrations = 0;
        private int _students = 0;
       
        private readonly List<int> _lessons = new List<int>();
        private readonly List<List<int>> _studentExams = new List<List<int>>();
        private readonly string[] _files = new string[13] {"car-f-92.stu", "car-s-91.stu", "ear-f-83.stu", "hec-s-92.stu", "kfu-s-93.stu", "lse-f-91.stu", "pur-s-93.stu", "rye-s-93.stu", "sta-f-83.stu", "tre-s-92.stu", "uta-s-92.stu", "ute-s-92.stu", "yor-f-83.stu" };
    }
}