using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using DemoLab.Factory;
using NUnit.Framework.Api;
using NUnit.Framework.Interfaces;

namespace DemoLab.Runner
{
    public class UserTestRunner : MarshalByRefObject
    {
        private readonly INUnitFilterFactory _filterFactory;

        public UserTestRunner(INUnitFilterFactory factory)
        {
            _filterFactory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public bool Run(byte[] testAssembly, string template, string testClassName, string testMethodName = null)
        {
            var asm = Assembly.Load(testAssembly);

            try
            {
                TestSourceFactory.Init(template);
            }
            catch (InvalidOperationException ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }

            var runner = new NUnitTestAssemblyRunner(new DefaultTestAssemblyBuilder());
            runner.Load(asm, new Dictionary<string, object>());
            var listener = new CustomListener();

            ITestFilter testFilter;

            if (string.IsNullOrEmpty(testMethodName))
            {
                testFilter = _filterFactory.TestFilterFromClass();
            }
            else
            {
                testFilter = _filterFactory.TestFilterFromClassAndMethod();
            }

            ITestResult result = runner.Run(listener, testFilter);
            ErrorMessage = listener.Message;
            return result.ResultState.Status != TestStatus.Failed;
        }

        public string ErrorMessage { get; private set; }


        public override object InitializeLifetimeService() => null;
    }

    public class CustomListener : ITestListener
    {
        public string Message { get; set; } = String.Empty;


        public void TestStarted(ITest test)
        {

        }

        public void TestFinished(ITestResult result)
        {
            if (result.ResultState.Status == TestStatus.Failed && string.IsNullOrEmpty(Message))
            {
                Message = result.Message;
            }
        }

        public void TestOutput(TestOutput output)
        {

        }
    }
}