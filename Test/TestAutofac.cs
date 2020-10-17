using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;

namespace Test
{

    /// <summary>
    /// Test for Autofac 
    /// </summary>
    public class TestAutofac
    {
        private static IContainer _container { get; set; }
        public void Run()
        {

            //basic pattern with IOC
            //create a containerbuilder,register components,  
            //build the container, create a lifetime scope from contianer and resolve the instances
            //
            var builder = new ContainerBuilder();

            //register by type
            builder.RegisterType<TestService>().As<ITestInterface>();

            //register by type with out interface
            builder.RegisterType<TestType>();

            //register in Lambda
            builder.Register(c => new TestLambService("Hello")).As<ITestLamb>();

            builder.Register(c => new TestLambServiceSec(c.Resolve<TestType>()));

            //register in Instance
            var testTypewith = new TestType("I am a instance");
            builder.RegisterInstance(testTypewith).As<TestType>();

            //constructor injection
            builder.Register(c => new TestType("I am a newer instance"));

            //property injection & not recommended
            builder.Register(c => new TestType() { _s = "I am a higher version" });

            //selection of an implementation by parameter value
            builder.Register<CreditCard>(
                (c, p) =>
                {
                    var accountid = p.Named<string>("_accountid");
                    if (accountid.StartsWith("9"))
                    {
                        return new GoldCard(accountid);
                    }
                    else
                    {
                        return new StandardCard(accountid);
                    }
                }
                );

            //register in open generic
            builder.RegisterGeneric(typeof(TestRepository<>))
                   .As(typeof(IRepository<>))
                   .InstancePerLifetimeScope();

            // register in closed generic
            builder.RegisterType<TestRepository2<TestClass>>().As<IRepository2<TestClass>>();
            
            // register in closed generic by delegate
            builder.RegisterGeneric((ctxt, types, parameters) =>
            {
                //官网教程这里没弄懂
                if (types.Contains(typeof(CreditCard)))
                {
                    return new TestRepository2<CreditCard>() ;
                }
                return Activator.CreateInstance(typeof(TestRepository2<>).MakeGenericType(types));
            }).As(typeof(IRepository2<>));


            _container = builder.Build();
            var scope = _container.BeginLifetimeScope();

            var testService = scope.Resolve<ITestInterface>();
            var testLambda = scope.Resolve<ITestLamb>();
            var testType = scope.Resolve<TestType>();
            var testLambdaSec = scope.Resolve<TestLambServiceSec>();
            var card = scope.Resolve<CreditCard>(new NamedParameter("_accountid", "12234"));
            var card2 = scope.Resolve<CreditCard>(new NamedParameter("_accountid", "992234"));
            var repos = scope.Resolve<IRepository<TestType>>();
            var repos2 = scope.Resolve<IRepository2<TestClass>>();
            var repos3 = scope.Resolve<IRepository2<CreditCard>>();
            

            testService.Log();
            testType.TypeLog();
            testLambda.LambLog();
            testLambdaSec.LambLogSec();
            card.Say();
            card2.Say();
            repos.SayRepository();
            repos2.SayRepository();
            repos3.SayRepository();
            Console.ReadKey();
        }
    }

    public interface ITestInterface
    {
        public void Log();
    }
    public class TestService : ITestInterface
    {
        public string _s;
        public TestService()
        {
            _s = "A TestService";
        }
        public void Log()
        {
            Console.WriteLine("Logging logs in TestType");
        }
    }

    public class TestService2 : ITestInterface
    {
        public string _s;
        public TestService2()
        {
            _s = "B TestService";
        }
        public void Log()
        {
            Console.WriteLine("Logging 2 logs in TestType");
        }
    }
    public interface ITestLamb
    {
        public void LambLog();
    }
    public class TestLambService : ITestLamb
    {
        public string _s;

        public TestLambService(string s)
        {
            _s = s;
        }
        public void LambLog()
        {
            Console.WriteLine($"Logging logs in TestLambda and _s is:{_s}");
        }
    }

    public class TestLambServiceSec
    {
        public string _t;
        public TestLambServiceSec(TestType testType)
        {
            _t = testType._s;
        }
        public void LambLogSec()
        {
            Console.WriteLine($"Logging logs in TestLambda and _t is:{_t}");
        }

    }

    public class TestLambServiceThi
    {
        public string _t;
        public TestLambServiceThi(TestType testType)
        {
            _t = testType._s;
        }
        public void LambLogSec()
        {
            Console.WriteLine($"Logging logs in TestLambdaThi and _t is:{_t}");
        }

    }
    public class TestType
    {
        public string _s;
        public TestType()
        {

        }
        public TestType(string s)
        {
            _s = s;
        }
        public void TypeLog()
        {
            Console.WriteLine($"Logging in TestType and string s is:{_s}");
        }
    }

    public abstract class CreditCard
    {
        public string _accountid;
        public string _name
        {
            get;
            set;
        }

        public CreditCard(string accountid)
        {
            _accountid = accountid;

        }


        public abstract void Say();
    }

    public class StandardCard : CreditCard
    {
        public StandardCard(string accountid) : base(accountid)
        {

        }
        public override void Say()
        {
            Console.WriteLine("I am a StandardCard");
        }
    }

    public class GoldCard : CreditCard
    {
        public GoldCard(string accountid) : base(accountid)
        {

        }
        public override void Say()
        {
            Console.WriteLine("I am a GoldCard");
        }
    }

    public interface IRepository<T>
    {
        void SayRepository();
    }

    public interface IRepository2<T>
    {
        void SayRepository();
    }
    public class TestRepository<T> : IRepository<T>
    {
       public static explicit operator TestRepository2<T>(TestRepository<T> obj)
        {
            return new TestRepository2<T>();
        }
       public void SayRepository()
        {
            Console.WriteLine($"I am A first ,the type of T is:{typeof(T).ToString()}");
        }
    }

    public class TestRepository2<T> : IRepository2<T>
    {

        public void SayRepository()
        {
            Console.WriteLine($"I am B first , the type of T is:{typeof(T).ToString()}");
        }
    }

    public class TestRepository3<T> : IRepository2<T>
    {

        public void SayRepository()
        {
            Console.WriteLine($"I am B second , the type of T is:{typeof(T).ToString()}");
        }
    }

    public class TestString
    {

    }
}
