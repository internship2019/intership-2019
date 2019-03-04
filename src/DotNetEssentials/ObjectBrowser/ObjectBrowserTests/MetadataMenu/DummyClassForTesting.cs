using System;

namespace ObjectBrowserTests.MetadataMenu
{
    /*
     * A class used for testing.
    **/   
    public class DummyClassForTesting
    {
        public int aVariable;

        public void MethodWithNoParams()
        {
        }

        public void MethodWithAParam(int theParam)
        {
        }

        public void MethodWithNullDefaultParameter(string theDefaultParameter = null)
        {
        }

        public void MethodWithDefaultParameter(int theDefaultParameter = 42)
        {
        }

        public void MethodWithOutParameter(out int theOutParameter)
        {
            theOutParameter = 42;
        }

        public void MethodWithStrParamType(string theStrParam)
        {
        }
    }
}
