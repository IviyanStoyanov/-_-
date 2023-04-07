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
                Stud = "���� ������",
                Grade = 6,
                Subject = "�����������",
                Teacher = "���� �������",
                Date = DateTime.Now
            };
            context.Marks.Add(newMark);
            context.SaveChanges();
           
            var searchMark = marksBusiness.GetMark(newMark.Id);

            Assert.AreNotEqual(newMark,searchMark,"GetMark doesn't return the searched mark.");

        }

        /// <summary>
        /// ���� ���������� ���� ������� AddMark �� MarksBusiness ������.
        /// </summary>
        [Test]
        public void AddMarkTesting()
        {
            var newMark = new Mark
            {
                Stud = "���� ������",
                Grade = 6,
                Subject = "�����������",
                Teacher = "���� �������",
                Date = DateTime.Now
            };

            marksBusiness.AddMark(newMark);
            var searchMark = context.Marks.OrderByDescending(x => x.Id).First();

            Assert.AreNotEqual(newMark, searchMark, "AddMark doesn't work.");
        }

        /// <summary>
        /// ���� ���������� ���� ������� UpdateMark �� MarksBusiness ������.
        /// </summary>
        [Test]
        public void UpdateMark()
        {
            var stud = new Mark 
            {
                Stud = "���� ������",
                Grade = 6,
                Subject = "�����������",
                Teacher = "���� �������",
                Date = DateTime.Now
            };
            context.Marks.Add(stud);
            context.SaveChanges();
            var changeOutput = context.Marks.OrderBy(c => c.Id).First();
            var expectedOutput = new Mark 
            {
                Stud = "���� ������",
                Grade = 5,
                Subject = "�����������",
                Teacher = "���� �������",
                Date = DateTime.Now
            };

            marksBusiness.UpdateMark(expectedOutput);
            context.Dispose();
            context = new Context();
            var output = context.Marks.Find(stud.Id);

            Assert.AreNotEqual(expectedOutput, output, "UpdateMark doesn't change the mark.");
        }

        /// <summary>
        /// ���� ���������� ���� ������� DeleteMark �� MarksBusiness ������.
        /// </summary>

        [Test]
        public void DeleteMark()
        {
            var stud = new Mark 
            { 
                Stud = "���� ������", 
                Grade = 6, Subject = "�����������",
                Teacher = "���� �������",
                Date = DateTime.Now
            };
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