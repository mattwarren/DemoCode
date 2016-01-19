using BenchmarkDotNet;
using BenchmarkDotNet.Plugins;
using BenchmarkDotNet.Plugins.Loggers;
using BenchmarkDotNet.Plugins.ResultExtenders;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace ImmutabilityBenchmark
{
    public class BenchmarkProgram
    {
        static void Main(string[] args)
        {
            // This currently doesn't work, because the BenchmarkBaselineDeltaResultExtender() is NEVER wired up!!!!!!
            //new BenchmarkRunner().Run<BenchmarkProgram>();

            var logger = new BenchmarkAccumulationLogger();
            var extender = new BenchmarkBaselineDeltaResultExtender();
            var plugins = BenchmarkPluginBuilder.CreateDefault()
                                .AddLogger(logger)
                                .AddResultExtender(extender)
                                .Build();
            var reports = new BenchmarkRunner(plugins).Run<BenchmarkProgram>();
        }

        [Benchmark(Baseline = true)]
        public MutablePerson MutablePOCO()
        {
            var jon = new MutablePerson
            {
                Name = "Jon",
                Address = new MutableAddress { City = "Reading", Street = "..." },
                Phones = { }
            };
            return jon;
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
        public Builder3.Person.Immutable Builder3()
        {
            var jon = new Builder3.Person
            {
                Name = "Jon",
                Address = new Builder3.Address { City = "Reading", Street = "..." },
                Phones = { }
            };
            return jon.ToImmutable();
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

        public sealed class MutablePerson
        {
            public string Name { get; set; }
            public MutableAddress Address { get; set; }
            public IReadOnlyList<MutablePhoneNumber> Phones { get; }

            public MutablePerson()
            {
                Phones = new List<MutablePhoneNumber>();
            }
        }

        public sealed class MutableAddress
        {
            public string Street { get; set; }
            public string City { get; set; }

            public MutableAddress()
            {
            }
        }

        public sealed class MutablePhoneNumber
        {
            public string Number { get; set; }
            public MutablePhoneNumberType Type { get; set; }

            public MutablePhoneNumber()
            {
            }
        }

        public enum MutablePhoneNumberType
        {
            Mobile = 0,
            Home = 1,
            Work = 2
        }
    }
}
