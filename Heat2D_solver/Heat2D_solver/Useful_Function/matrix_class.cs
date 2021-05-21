using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heat2D_solver.Useful_Function
{
    public class matrix_class
    {
        private double[,] mInnerMatrix;
        private int mRowCount = 0;
        public int RowCount
        {
            get
            {
                return mRowCount;
            }
        }
        private int mColumnCount = 0;
        public int ColumnCount
        {
            get
            {
                return mColumnCount;
            }
        }

        public double[,] return_matrix_as_double
        {
            get { return mInnerMatrix; }
        }

        public matrix_class()
        {
        }
        public matrix_class(int rowCount, int columnCount)
        {
            mRowCount = rowCount;
            mColumnCount = columnCount;
            mInnerMatrix = new double[rowCount - 1 + 1, columnCount - 1 + 1];
        }

        public double this[int rowNumber, int columnNumber]
        {
            get
            {
                return mInnerMatrix[rowNumber, columnNumber];
            }
            set
            {
                mInnerMatrix[rowNumber, columnNumber] = value;
            }
        }

        public double[] GetRow(int rowIndex)
        {
            double[] rowValues = new double[mColumnCount - 1 + 1];
            for (int i = 0; i <= mColumnCount - 1; i++)
                rowValues[i] = mInnerMatrix[rowIndex, i];
            return rowValues;
        }
        public void SetRow(int rowIndex, double[] value)
        {
            if (value.Length != mColumnCount)
                throw new Exception("Size Mismatch");
            for (int i = 0; i <= value.Length - 1; i++)
                mInnerMatrix[rowIndex, i] = value[i];
        }
        public double[] GetColumn(int columnIndex)
        {
            double[] columnValues = new double[mRowCount - 1 + 1];
            for (int i = 0; i <= mRowCount - 1; i++)
                columnValues[i] = mInnerMatrix[i, columnIndex];
            return columnValues;
        }
        public void SetColumn(int columnIndex, double[] value)
        {
            if (value.Length != mRowCount)
                throw new Exception("Size Mismatch");
            for (int i = 0; i <= value.Length - 1; i++)
                mInnerMatrix[i, columnIndex] = value[i];
        }


        public static matrix_class operator +(matrix_class pMatrix1, matrix_class pMatrix2)
        {
            if (!(pMatrix1.RowCount == pMatrix2.RowCount && pMatrix1.ColumnCount == pMatrix2.ColumnCount))
                throw new Exception("Size Mismatch");
            matrix_class returnMartix = new matrix_class(pMatrix1.RowCount, pMatrix2.ColumnCount);
            for (int i = 0; i <= pMatrix1.RowCount - 1; i++)
            {
                for (int j = 0; j <= pMatrix1.ColumnCount - 1; j++)
                    returnMartix[i, j] = pMatrix1[i, j] + pMatrix2[i, j];
            }
            return returnMartix;
        }
        public static matrix_class operator *(double scalarValue, matrix_class pMatrix)
        {
            matrix_class returnMartix = new matrix_class(pMatrix.RowCount, pMatrix.ColumnCount);
            for (int i = 0; i <= pMatrix.RowCount - 1; i++)
            {
                for (int j = 0; j <= pMatrix.ColumnCount - 1; j++)
                    returnMartix[i, j] = pMatrix[i, j] * scalarValue;
            }
            return returnMartix;
        }
        public static matrix_class operator -(matrix_class pMatrix1, matrix_class pMatrix2)
        {
            if (!(pMatrix1.RowCount == pMatrix2.RowCount && pMatrix1.ColumnCount == pMatrix2.ColumnCount))
                throw new Exception("Size Mismatch");
            return pMatrix1 + -1 * pMatrix2;
        }
        public static bool operator ==(matrix_class pMatrix1, matrix_class pMatrix2)
        {
            if (!(pMatrix1.RowCount == pMatrix2.RowCount && pMatrix1.ColumnCount == pMatrix2.ColumnCount))
                // Size Mismatch
                return false;
            for (int i = 0; i <= pMatrix1.RowCount - 1; i++)
            {
                for (int j = 0; j <= pMatrix1.ColumnCount - 1; j++)
                {
                    if (pMatrix1[i, j] != pMatrix2[i, j])
                        return false;
                }
            }
            return true;
        }
        public static bool operator !=(matrix_class pMatrix1, matrix_class pMatrix2)
        {
            return !(pMatrix1 == pMatrix2);
        }
        public static matrix_class operator -(matrix_class pMatrix)
        {
            return -1 * pMatrix;
        }
        // Public Shared Operator +=(pMatrix As Matrix) As Matrix

        // For i As Integer = 0 To pMatrix.RowCount - 1
        // For j As Integer = 0 To pMatrix.ColumnCount - 1
        // pMatrix(i, j) += 1
        // Next
        // Next
        // Return pMatrix
        // End Operator
        // Public Shared Operator -=(pMatrix As Matrix) As Matrix
        // For i As Integer = 0 To pMatrix.RowCount - 1
        // For j As Integer = 0 To pMatrix.ColumnCount - 1
        // pMatrix(i, j) -= 1
        // Next
        // Next
        // Return pMatrix
        // End Operator
        public static matrix_class operator *(matrix_class pMatrix1, matrix_class pMatrix2)
        {
            if (pMatrix1.ColumnCount != pMatrix2.RowCount)
                throw new Exception("Size Mismatch");
            matrix_class returnMatrix = new matrix_class(pMatrix1.RowCount, pMatrix2.ColumnCount);
            for (int i = 0; i <= pMatrix1.RowCount - 1; i++)
            {
                double[] rowValues = pMatrix1.GetRow(i);
                for (int j = 0; j <= pMatrix2.ColumnCount - 1; j++)
                {
                    double[] columnValues = pMatrix2.GetColumn(j);
                    double value = 0;
                    for (int a = 0; a <= rowValues.Length - 1; a++)
                        value += rowValues[a] * columnValues[a];
                    returnMatrix[i, j] = value;
                }
            }
            return returnMatrix;
        }
        public matrix_class Transpose()
        {
            matrix_class mReturnMartix = new matrix_class(ColumnCount, RowCount);
            for (int i = 0; i <= mRowCount - 1; i++)
            {
                for (int j = 0; j <= mColumnCount - 1; j++)
                    mReturnMartix[j, i] = mInnerMatrix[i, j];
            }
            return mReturnMartix;
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool IsZeroMatrix()
        {
            for (int i = 0; i <= RowCount - 1; i++)
            {
                for (int j = 0; j <= ColumnCount - 1; j++)
                {
                    if (mInnerMatrix[i, j] != 0)
                        return false;
                }
            }
            return true;
        }
        public bool IsSquareMatrix()
        {
            return RowCount == ColumnCount;
        }
        public bool IsLowerTriangle()
        {
            if (!IsSquareMatrix())
                return false;
            for (int i = 0; i <= RowCount - 1; i++)
            {
                for (int j = i + 1; j <= ColumnCount - 1; j++)
                {
                    if (mInnerMatrix[i, j] != 0)
                        return false;
                }
            }
            return true;
        }
        public bool IsUpperTriangle()
        {
            if (!IsSquareMatrix())
                return false;
            for (int i = 0; i <= RowCount - 1; i++)
            {
                for (int j = 0; j <= i - 1; j++)
                {
                    if (mInnerMatrix[i, j] != 0)
                        return false;
                }
            }
            return true;
        }
        public bool IsDiagonalMatrix()
        {
            if (!IsSquareMatrix())
                return false;
            for (int i = 0; i <= RowCount - 1; i++)
            {
                for (int j = 0; j <= ColumnCount - 1; j++)
                {
                    if (i != j && mInnerMatrix[i, j] != 0)
                        return false;
                }
            }
            return true;
        }
        public bool IsIdentityMatrix()
        {
            if (!IsSquareMatrix())
                return false;
            for (int i = 0; i <= RowCount - 1; i++)
            {
                for (int j = 0; j <= ColumnCount - 1; j++)
                {
                    double checkValue = 0;
                    if (i == j)
                        checkValue = 1;
                    if (mInnerMatrix[i, j] != checkValue)
                        return false;
                }
            }
            return true;
        }
        public bool IsSymetricMatrix()
        {
            if (!IsSquareMatrix())
                return false;
            matrix_class transposeMatrix = Transpose();
            if ((this) == transposeMatrix)
                return true;
            else
                return false;
        }
        public string print_matrix()
        {
            int i, j;
            string str = "\n";
            for (i = 0; i < this.RowCount; i++)
            {
                str = str + "|";
                for (j = 0; j < this.ColumnCount; j++)
                {
                    str = str + "\t" + this[i, j].ToString("N8");
                }
                str = str + "\t" + "|" + "\n";
            }
            str = str + "\n";
            return str;
        }

        //Public Function Print_Matrix() As String
        //    Dim Str As String = vbNewLine
        //    For i = 0 To(Me.RowCount - 1) Step +1
        //        Str = Str & "|"
        //        For j = 0 To(Me.ColumnCount - 1) Step +1
        //            Str = Str & vbTab & (Me.Item(i, j).ToString("N8"))
        //        Next
        //        Str = Str & vbTab & "|" & vbNewLine
        //    Next
        //    Str = Str & vbNewLine
        //    Return Str
        //End Function

    }
}
