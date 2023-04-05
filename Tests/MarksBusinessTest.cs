using Business;
using Data;
using Data.Model;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Newtonsoft.Json;

/// <summary>
/// ������������ � ����� �������� Tests.
/// </summary>
namespace Tests
{
    /// <summary>
    /// �������� ���� � NUnit ������� �� �������������� MarksBusiness.
    /// </summary>
    public class MarksBusinessTests
    {
        private Context context;
        private MarksBusiness marksBusiness;

        /// <summary>
        /// �������� �� �������� � studentBusiness
        /// </summary>
        [SetUp]
        public void Setup()
        {
            context = new Context();
            marksBusiness = new MarksBusiness();
        }

        /// <summary>
        /// ���� ���������� ���� ������� GetAllMarks �� MarksBusiness ������.
        /// </summary>
        [Test]
        public void GetAllMarksTesting()
        {
            var newMarks = JsonConvert.SerializeObject(context.Marks.OrderBy(x => x.Id));

            var searchMarks = JsonConvert.SerializeObject(marksBusiness.GetAllMarks().OrderBy(x => x.Id));

            CollectionAssert.AreEqual(newMarks, searchMarks);
        }

        /// <summary>
        /// ���� ���������� ���� ������� GetMark �� MarksBusiness ������.
        /// </summary>
        [Test]
        public void GetMarkTesting()
        {
            var newMark = new Mark
            {
                Stud = "Katerina Slavova",
                Grade = 6,
                Subject = "Math",
                Teacher = "Tanq Zlateva",
                Date = DateTime.Now
            };
            context.Marks.Add(newMark);
            context.SaveChanges();
           
            var resultMark = marksBusiness.GetMark(newMark.Id);

            Assert.AreEqual(newMark,resultMark);

        }

        /// <summary>
        /// ���� ���������� ���� ������� AddMark �� MarksBusiness ������.
        /// </summary>
        [Test]
        public void AddMarkTesting()
        {
            var newMark = new Mark
            {
                Stud = "Katerina Slavova",
                Grade = 6,
                Subject = "Math",
                Teacher = "Tanq Zlateva",
                Date = DateTime.Now
            };

            marksBusiness.AddMark(newMark);
            var searchMark = context.Marks.OrderByDescending(x => x.Id).First();

            Assert.AreEqual(newMark.Id, searchMark.Id);
            Assert.AreEqual(newMark.Stud, searchMark.Stud);
            Assert.AreEqual(newMark.Subject, searchMark.Subject);
            Assert.AreEqual(newMark.Teacher, searchMark.Teacher);
            Assert.AreEqual(newMark.Date, searchMark.Date);
        }

        /// <summary>
        /// ���� ���������� ���� ������� UpdateMark �� MarksBusiness ������.
        /// </summary>
        [Test]
        public void UpdateMark()
        {
            var stud = new Mark { Stud = "Iviyan", Grade = 6, Subject = "IT", Teacher = "T.Ivanova", Date = DateTime.Now };
            context.Marks.Add(stud);
            context.SaveChanges();
            var changeOutput = context.Marks.OrderBy(c => c.Id).First();
            var expectedOutput = new Mark { Stud = "Iviyan", Grade = 6, Subject = "IT", Teacher = "T.Ivanova", Date = DateTime.Now };

            marksBusiness.UpdateMark(expectedOutput);
            context.Dispose();
            context = new Context();
            var output = context.Marks.Find(stud.Id);

        
            Assert.AreEqual(expectedOutput.Stud, output.Stud);
            Assert.AreEqual(expectedOutput.Subject, output.Subject);
            Assert.AreEqual(expectedOutput.Teacher, output.Teacher);
        }

        /// <summary>
        /// ���� ���������� ���� ������� DeleteMark �� MarksBusiness ������.
        /// </summary>

        [Test]
        public void DeleteMark()
        {
            var stud = new Mark { Stud = "Iviyan", Grade = 6, Subject = "IT", Teacher = "T.Ivanova", Date = DateTime.Now };
            context.Marks.Add(stud);
            context.SaveChanges();

            marksBusiness.DeleteMark(stud.Id);
            context.Dispose();
            context = new Context();
            var actualStud = context.Students.Find(stud.Id);

            Assert.IsNull(actualStud);
            context.SaveChanges();
        }

    }
}