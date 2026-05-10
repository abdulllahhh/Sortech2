using Domain.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class InMemoryDataStore
    {
        public static ConcurrentDictionary<int, Student> Students { get; } = new();
        public static ConcurrentDictionary<int, Class> Classes { get; } = new();
        public static ConcurrentDictionary<int, Enrollment> Enrolments { get; } = new();
        public static ConcurrentDictionary<int, Mark> Marks { get; } = new();

        private static int _studentIdCounter = 0;
        private static int _classIdCounter = 0;
        private static int _enrolmentIdCounter = 0;
        private static int _markIdCounter = 0;

        public static int GetNextStudentId() => Interlocked.Increment(ref _studentIdCounter);
        public static int GetNextClassId() => Interlocked.Increment(ref _classIdCounter);
        public static int GetNextEnrolmentId() => Interlocked.Increment(ref _enrolmentIdCounter);
        public static int GetNextMarkId() => Interlocked.Increment(ref _markIdCounter);

    }
}
