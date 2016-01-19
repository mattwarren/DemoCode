using BenchmarkDotNet;
using System.Collections.Immutable;

namespace ImmutabilityBenchmark
{
    public class BenchmarkProgram
    {
        static void Main(string[] args)
        {
            new BenchmarkRunner().Run<BenchmarkProgram>();
        }

        [Benchmark]
        public Builder1.Person Builder1()
        {
            var jon = new Builder1.Person.Builder
            {
                Name = "Jon",
                Address = new Builder1.Address.Builder { City = "Reading", Street = "..." }.Build(),
                Phones = { }
            }.Build();
            return jon;
        }

        [Benchmark]
        public Builder2.Person Builder2()
        {
            var jon = new Builder2.Person.Builder
            {
                Name = "Jon",
                Address = new Builder2.Address.Builder { City = "Reading", Street = "..." }.Build(),
                Phones = { }
            }.Build();
            return jon;
        }

        [Benchmark]
        public Builder3.Person Builder3()
        {
            var jon = new Builder3.Person
            {
                Name = "Jon",
                Address = new Builder3.Address { City = "Reading", Street = "..." },
                Phones = { }
            };
            return jon;
        }

        [Benchmark("Withers")]
        public Withers.Person WithersBenchmark()
        {
            var jon = new Withers.Person(
                name: "Foo",
                address: null, // Do this later
                phones: new[] { new Withers.PhoneNumber("1235", Withers.PhoneNumberType.Home) }.ToImmutableList());
            var later = jon.WithAddress(new Withers.Address("School Road", "Reading"));
            return jon;
        }
    }
}
