using Business;
using Data;
using Data.Model;
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


        }

        /// <summary>
        /// ���� ���������� ���� ������� AddMark �� MarksBusiness ������.
        /// </summary>
        [Test]
        public void AddMarkTesting()
        {
            var newMark = new Mark
            {
                Id = 0,
                Stud = "Katerina Slavova",
                Grade = 11,
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
            var stud = new Mark { Grade = 6, Id = 0, Subject = "IT", Stud = "Iviyan", Date = DateTime.Now, Teacher = "T.Ivanova", };
            context.Marks.Add(stud);
            context.SaveChanges();
            var changeOutput = context.Marks.OrderBy(c => c.Id).First();
            var expectedOutput = new Mark { Id = 0, Stud = "Iviyan", Grade = 6, Subject = "IT", Teacher = "T.Ivanova", Date = DateTime.Now };

            marksBusiness.UpdateMark(expectedOutput);
            context.Dispose();
            context = new Context();
            var output = context.Marks.Find(stud.Id);


            Assert.AreEqual(expectedOutput.Id, output.Id);
            Assert.AreEqual(expectedOutput.Stud, output.Stud);
            Assert.AreEqual(expectedOutput.Subject, output.Subject);
            Assert.AreEqual(expectedOutput.Teacher, output.Teacher);
            Assert.AreEqual(expectedOutput.Date, output.Date);

        }

        /// <summary>
        /// ���� ���������� ���� ������� DeleteMark �� MarksBusiness ������.
        /// </summary>

        [Test]
        public void DeleteMark()
        {
            var stud = new Mark { Id = 0, Stud = "Iviyan", Grade = 6, Subject = "IT", Teacher = "T.Ivanova", Date = DateTime.Now };
            context.Marks.Add(stud);
            context.SaveChanges();

            marksBusiness.DeleteMark(stud.Id);
            context.Dispose();
            context = new Context();
            var actualCar = context.Students.Find(stud.Id);

            Assert.IsNull(actualCar);
            context.SaveChanges();
        }

    }
}