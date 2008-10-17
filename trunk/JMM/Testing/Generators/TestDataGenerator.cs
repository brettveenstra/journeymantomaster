#region copyright

// Copyright (c) 2008, Brett L. Veenstra
// 
// Redistribution and use in source and binary forms, with or without 
// modification, are permitted provided that the following conditions 
// are met:
// 
//     * Redistributions of source code must retain the above copyright 
// 		notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright 
// 		notice, this list of conditions and the following disclaimer in 
// 		the documentation and/or other materials provided with the 
// 		distribution.
//     * The names of its contributors may be used to endorse or promote 
// 		products derived from this software without specific prior 
// 		written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS 
// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT 
// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR 
// A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT 
// OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, 
// SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED 
// TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
// PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF 
// LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
// NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS 
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
// 

#endregion

using JMM.Testing.Factory;

namespace JMM.Testing.Generators
{
    public class TestDataGenerator<T> : ITestDataGenerator<T>
    {
        private readonly T m_For;
        private bool m_DbNullDefaultsToNull;
        private int m_Occurs = 1;

        private TestDataGenerator(T generateFor)
        {
            m_For = generateFor;
        }

        public static TestDataGenerator<T> For(T generateFor)
        {
            return new TestDataGenerator<T>(generateFor);
        }

        public TestDataGenerator<T> Occurs(int occurs)
        {
            m_Occurs = occurs;
            return this;
        }

        public T Generate()
        {
            return TestDataGeneratorFactory.CreateFor<T>().Generate(m_Occurs, m_For, m_DbNullDefaultsToNull);
        }

        public TestDataGenerator<T> DbNullDefaultsToNull()
        {
            m_DbNullDefaultsToNull = true;
            return this;
        }
    }
}