using System;
using System.IO;
using Xunit;

namespace DependencyInjectionExample.UnitTests
{
    public class ContainerTests
    {
        public ContainerTests()
        {
            if (File.Exists(FileLogger.Path)) File.Delete(FileLogger.Path);
        }

        private class MyMother
        {
            public Guid Id {get;} = Guid.NewGuid();
            public int value = 0;
        }

        private class MyChild : MyMother
        {
            public MyChild()
            {
                value = 10;
            }
        }

        private interface MyInterface {}

        private class MyImplementation : MyInterface {}

        [Fact]
        public void RetrievesInstanceOfInterface()
        {
            var container = new Container();
            container.RegisterType<MyInterface, MyImplementation>();

            var resolved = container.Resolve<MyInterface>();
            Assert.NotNull(resolved);
        }

        [Fact]
        public void RetrievesInstanceOfChildClass()
        {
            var container = new Container();
            container.RegisterType<MyMother, MyChild>();

            var resolved = container.Resolve<MyMother>();
            Assert.NotNull(resolved);
            Assert.Equal(10, resolved.value);
        }

        [Fact]
        public void InjectsFileLoggerToService()
        {
        //Given
            var container = new Container();
            container.RegisterType<ILogger, FileLogger>();

        //When
            var logger = container.Resolve<ILogger>();
            new Service(logger).Run();
        
        //Then
            Assert.True(File.Exists(FileLogger.Path));
            Assert.True(File.ReadAllText(FileLogger.Path).IndexOf("Hello world!") != -1);
            File.Delete(FileLogger.Path);
        }

        [Fact]
        public void InjectsConsoleLoggerToService()
        {
        //Arrange
            var oldConsoleOut = Console.Out;
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

        //Given
            var container = new Container();
            container.RegisterType<ILogger, ConsoleLogger>();

        //When
            var logger = container.Resolve<ILogger>();
            new Service(logger).Run();
        
        //Then
            var result = stringWriter.ToString();
            Assert.True(result.IndexOf("Hello world!") != -1);

            Console.SetOut(oldConsoleOut);
        }

        [Fact]
        public void ReturnAlwaysSameInstance()
        {
            var container = new Container();
            container.RegisterType<MyMother, MyChild>();
            var m1 = container.Resolve<MyMother>();
            var m2 = container.Resolve<MyMother>();

            Assert.True(m1.Id == m2.Id);
        }

        [Fact]
        public void ThrowsExceptionIfTypeNotRegistered()
        {
            var container = new Container();

            try {
                container.Resolve<MyChild>();
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
                Assert.True(true);
            }
        }
    }
}
