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

using System;
using System.Data;

namespace JMM.Testing.DataProviders
{
    public class DataTableTestDataProvider : ITestDataProvider<DataTable>
    {
        #region ITestDataProvider<DataTable> Members

        public DataTable Generate(int occurs, DataTable dataTable, bool dbNullDefaultsToNull)
        {
            dataTable.Rows.Clear();

            for (int i = 0; i < occurs; i++)
            {
                DataRow dataRow = dataTable.NewRow();

                foreach (DataColumn column in dataTable.Columns)
                {
                    DataColumnTestDataProvider provider = new DataColumnTestDataProvider(column, dbNullDefaultsToNull);
                    dataRow[column.ColumnName] = provider.Generate();
                }

                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }

        #endregion

        #region Nested type: DataColumnTestDataProvider

        public class DataColumnTestDataProvider
        {
            private readonly DataColumn m_Column;
            private readonly bool m_DbNullDefaultsToNull;

            public DataColumnTestDataProvider(DataColumn column, bool dbNullDefaultsToNull)
            {
                m_Column = column;
                m_DbNullDefaultsToNull = dbNullDefaultsToNull;
            }

            public object Generate()
            {
                if (m_Column.AllowDBNull
                    && m_Column.AutoIncrement == false
                    && m_DbNullDefaultsToNull)
                {
                    return DBNull.Value;
                }

                if (typeof (string)
                    == m_Column.DataType)
                {
                    if (m_Column.MaxLength > 0)
                    {
                        return "foobarbazbiz".Substring(0, m_Column.MaxLength);
                    }
                    return "foobarbazbiz";
                }

                if (typeof (int)
                    == m_Column.DataType
                        )
                {
                    return new Random((int) ( m_Column.ColumnName.Length - DateTime.Now.Ticks )).Next();
                }

                if (typeof (long)
                    == m_Column.DataType)
                {
                    return DateTime.Now.Ticks;
                }

                if (typeof (DateTime)
                    == m_Column.DataType)
                {
                    return new DateTime(DateTime.Now.Ticks - new Random(m_Column.ColumnName.Length).Next());
                }

                throw new ArgumentOutOfRangeException(string.Format("Could not handle type [{0}]", m_Column.DataType));
            }
        }

        #endregion
    }
}