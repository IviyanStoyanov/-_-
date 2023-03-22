﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Пространство с имена наречено Data.Model.
/// </summary>
namespace Data.Model
{
    /// <summary>
    /// Клас Student, с атрибути Id, Name, Email, Grade.
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Задване на номер на ученика.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Задаване на име на ученика.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Задаване на имейл на ученика.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Задаване на оценки на ученика.
        /// </summary>
        public int Grade { get; set; }


        /// <summary>
        /// Задаване на рожден ден на ученика.
        /// </summary>
        public int birthDate { get; set; }
    }
}
