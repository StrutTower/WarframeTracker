using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace WarframeTrackerTests.TestObjects {
    public class TestObject : IEquatable<TestObject> {
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime Timestamp { get; set; }
        
        public bool IsActive { get; set; }


        public override bool Equals(object obj) {
            return obj is TestObject other &&
                ID == other.ID &&
                Name == other.Name &&
                Timestamp == other.Timestamp &&
                IsActive == other.IsActive;
        }

        public bool Equals([AllowNull] TestObject other) {
            return other != null && ID == other.ID;
        }

        public override int GetHashCode() {
            return ID.GetHashCode();
        }
    }
}
