using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace OOD_LW5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ////////////////////// global data ///////////////////////////////////
      
        string setUniversum = "abcdefghijklmnopqrstuvwxyz";
        string textFromFile = "";

        ////////////////////// prepare data ///////////////////////////////////

        private void initDataByManual()
        {
            clearData();
            textBox1.ReadOnly = false;
            textBox2.ReadOnly = false;
            textBox1.BackColor = Color.White;
            textBox2.BackColor = Color.White;
        }

        private void initDataFromFile()
        {
            string setA = "";
            string setB = "";

            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Stream myStream;
                if ((myStream = ofd.OpenFile()) != null)
                {
                    string fn = ofd.FileName;
                    textFromFile = File.ReadAllText(fn);
                }
                myStream.Close();
            }

            if (textFromFile.Length != 0)
            {
                int firstPos = textFromFile.IndexOf('|');

                if (firstPos == 0)
                {
                    setB = textFromFile.Substring(firstPos + 1);
                }
                else if (firstPos == (textFromFile.Length - 1))
                {
                    setA = textFromFile.Substring(0, firstPos - 1);
                }
                else if (firstPos == -1)
                {
                    setA = textFromFile;
                }
                else
                {
                    setA = textFromFile.Substring(0, firstPos);
                    setB = textFromFile.Substring(firstPos + 1);
                }
            }

            textBox1.Text = setA;
            textBox2.Text = setB;
        }

        private string initDataByRendom(int seed)
        {
            Random rnd = new Random((int)DateTime.Now.Millisecond + seed);
            int rundLength = rnd.Next(7);// от 0 до 26 (27 не включается)
            string rundString = "";
            char rundChar = '\0';

            for (int i = 0; i < rundLength; i++)
            {
                rundChar = Convert.ToChar(rnd.Next(97, 123));//от 97 до 122 (123 не включается)
                rundString += Convert.ToString(rundChar);
            }

            rundString = new string(rundString.Distinct().ToArray());
            rundString = string.Concat(rundString.OrderBy(x => x).ToArray());
            return rundString;
        }

        private bool checkData(string a, string b)
        {
            int cntError = 0;
            bool errorPresent = false;

            // chech string a
            foreach (char ch in a)
            {
                if (Convert.ToInt32(ch) < 97 || Convert.ToInt32(ch) > 122)
                {
                    cntError++;
                }
            }

            // chech string b
            foreach (char ch in b)
            {
                if (Convert.ToInt32(ch) < 97 || Convert.ToInt32(ch) > 122)
                {
                    cntError++;
                }
            }

            if (cntError > 0)
            {
                errorPresent = true;
            }
            return errorPresent;
        }

        private void clearData()
        {
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";

            label3.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            textBox3.Visible = false;
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;
            pictureBox7.Visible = false;
            pictureBox8.Visible = false;
            pictureBox9.Visible = false;
            pictureBox10.Visible = false;
            pictureBox11.Visible = false;
            pictureBox12.Visible = false;
            pictureBox13.Visible = false;
            pictureBox14.Visible = false;
            pictureBox15.Visible = false;

            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox1.BackColor = Color.AntiqueWhite;
            textBox2.BackColor = Color.AntiqueWhite;
        }

        private void DisableEdit()
        {
            label5.Visible = true;
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox1.BackColor = Color.AntiqueWhite;
            textBox2.BackColor = Color.AntiqueWhite;
        }

        private bool IsCross(string sa, string sb)
        {
            int tmp = 0;
            bool crossYES = false;

            foreach (char chA in sa)
            {
                foreach (char chB in sb)
                {
                    if (chA == chB)
                    {
                        tmp++;
                    }
                }
            }

            if (tmp > 0)
            {
                crossYES = true;
            }

            return crossYES;
        }

        private bool ItsIncludes(string sa, string sb)// a in b
        {
            int tmp = 0;
            bool сontainsYES = false;

            foreach (char chA in sa)
            {
                foreach (char chB in sb)
                {
                    if (chA == chB)
                    {
                        tmp++;
                    }
                }
            }

            if (tmp == sa.Length)
            {
                сontainsYES = true;
            }

            return сontainsYES;
        }

        ////////////////////// calculate data /////////////////////////////////

        private string CalculateUnion(string setA, string setB)
        {
            string resultUnion = "";

            setA = new string(setA.Distinct().ToArray());
            setB = new string(setB.Distinct().ToArray());
            setA = string.Concat(setA.OrderBy(x => x).ToArray());
            setB = string.Concat(setB.OrderBy(x => x).ToArray());

            if (checkData(setA, setB))
            {
                resultUnion = "";
            }
            else
            {
                if (setA == "" && setB =="")
                {
                    resultUnion="";
                }
                else if (setA == setUniversum && setB==setUniversum)
                {
                    resultUnion = setUniversum;
                }
                else if ((setA == "" && setB == setUniversum) ||
                         (setA == setUniversum && setB == "" ))
                {
                    resultUnion = setUniversum;
                }
                else if ((setA != setUniversum && setB == "") ||
                         (setA == "" && setB != setUniversum) )
                {
                    resultUnion = setA;
                }
                else if ((setA != "" && setB == setUniversum) ||
                         (setA == setUniversum && setB != ""))
                {
                    resultUnion = setUniversum;
                }
                else if(setA == setB)
                {
                    resultUnion = setA;
                }
                else///////////////////////////////////////////
                {
                    resultUnion = setA + setB;
                    resultUnion = new string(resultUnion.Distinct().ToArray());
                    resultUnion = string.Concat(resultUnion.OrderBy(x => x).ToArray());

                    if (resultUnion == setUniversum)
                    {
                        resultUnion = setUniversum;
                    }
                }
            }
            return resultUnion;
        }

        private string CalculateIntersection(string setA, string setB)
        {
            string resultIntersection = "";

            setA = new string(setA.Distinct().ToArray());
            setB = new string(setB.Distinct().ToArray());
            setA = string.Concat(setA.OrderBy(x => x).ToArray());
            setB = string.Concat(setB.OrderBy(x => x).ToArray());

            int lengthA = setA.Length;
            int lengthB = setB.Length;

            if (checkData(setA, setB))
            {
                resultIntersection = "";
            }
            else
            {
                if (setA == "" && setB =="")// o o
                {
                    resultIntersection = "";
                }
                else if (setA == setUniversum && setB==setUniversum)// u u
                {
                    resultIntersection = setUniversum;
                }
                else if ((setA == "" && setB == setUniversum) ||
                         (setA == setUniversum && setB == "" ))// ou uo
                {
                    resultIntersection = "";
                }
                else if (((setA != setUniversum && setA!="" ) && setB == "") ||
                          (setA == "" && (setB != setUniversum && setB !="")) )//ao oa
                {
                    resultIntersection = "";
                }
                else if (((setA != "" && setA != setUniversum) && setB == setUniversum))//au
                {
                    resultIntersection = setA;
                }
                else if ((setA == setUniversum && (setB != "" && setB != setUniversum)))//ub
                {
                    resultIntersection = setB;
                }
                else if(setA == setB)// a a
                {
                    resultIntersection = setA;
                }
                else/////////////////////////////////////////////////
                {
                    // взять общую часть
                    foreach (char chA in setA)
                    {
                        foreach (char chB in setB)
                        {
                            if (chA == chB)
                            {
                                resultIntersection += Convert.ToString(chA);
                            }
                        }
                    }

                    resultIntersection = new string(resultIntersection.Distinct().ToArray());
                    resultIntersection = string.Concat(resultIntersection.OrderBy(x => x).ToArray());
                }
            }
            return resultIntersection;
        }

        private string CalculateDifference(string setA, string setB)
        {
            string resultDifference = "";

            setA = new string(setA.Distinct().ToArray());
            setB = new string(setB.Distinct().ToArray());
            setA = string.Concat(setA.OrderBy(x => x).ToArray());
            setB = string.Concat(setB.OrderBy(x => x).ToArray());

            if (checkData(setA, setB))
            {
                resultDifference = "";
            }
            else
            {
                if (setA == "" && setB == "")
                {
                    resultDifference = "";
                }
                else if (setA == setUniversum && setB == setUniversum)
                {
                    resultDifference = "";
                }
                else if (setA == "" && setB == setUniversum)
                {
                    resultDifference = "";
                }
                else if (setA == setUniversum && setB == "")
                {
                    resultDifference = setUniversum;
                }
                else if ((setA != setUniversum && setA !="") && setB == "")
                {
                    resultDifference = setA;
                }
                else if(setA == "" && (setB != setUniversum && setB != ""))
                {
                    resultDifference = "";
                }
                else if ((setA != "" && setA!=setUniversum ) && setB == setUniversum)
                {
                    resultDifference = "";
                }
                else if(setA == setUniversum && (setB != "" && setB!= setUniversum))
                {
                    resultDifference = setUniversum;
                    //============= not B======================
                    for (int i = 0; i < resultDifference.Length; i++)
                    {
                        foreach (char cр in setB)
                        {
                            if (cр == resultDifference[i])
                            {
                                resultDifference = resultDifference.Remove(i, 1);
                            }
                        }
                    }
                }
                else if (setA == setB)
                {
                    resultDifference = "";
                }
                else////////////////////////////////////////////////
                {
                    if (ItsIncludes(setA, setB))// a in b
                    {
                        resultDifference = "";
                    }
                    else if (ItsIncludes(setB, setA))// b in a
                    {
                        resultDifference = setA;
                        //============= not B======================
                        for (int i = 0; i < resultDifference.Length; i++)
                        {
                            foreach (char cр in setB)
                            {
                                if (cр == resultDifference[i])
                                {
                                    resultDifference = resultDifference.Remove(i, 1);
                                }
                            }
                        }
                    }
                    else
                    {
                        //пересекаются, не пересекаются
                        if (IsCross(setA, setB))
                        {
                            //пересекаются (a\b)
                            string tmp = "";//пересекающаяся часть
                            foreach (char chA in setA)
                            {
                                foreach (char chB in setB)
                                {
                                    if (chA == chB)
                                    {
                                        tmp += Convert.ToString(chA);
                                    }
                                }
                            }

                            tmp = new string(tmp.Distinct().ToArray());
                            tmp = string.Concat(tmp.OrderBy(x => x).ToArray());

                            // из а вычесть пересекающуюся часть
                            resultDifference = setA;
                            for (int i = 0; i < resultDifference.Length; i++)
                            {
                                foreach (char ch in tmp)
                                {
                                    if (ch == resultDifference[i])
                                    {
                                        resultDifference = resultDifference.Remove(i, 1);
                                    }
                                }
                            }
                        }
                        else
                        {
                            //не пересекаются
                            resultDifference = "";
                        }
                    }
                }
            }

            return resultDifference;
        }

        private string CalculateSymmetricDifference(string setA, string setB)
        {
            string resultSymmetricDifference = "";

            setA = new string(setA.Distinct().ToArray());
            setB = new string(setB.Distinct().ToArray());
            setA = string.Concat(setA.OrderBy(x => x).ToArray());
            setB = string.Concat(setB.OrderBy(x => x).ToArray());

            if (checkData(setA, setB))
            {
                resultSymmetricDifference = "";
            }
            else
            {
                if (setA == "" && setB == "")
                {
                    resultSymmetricDifference = "";
                }
                else if (setA == setUniversum && setB == setUniversum)
                {
                    resultSymmetricDifference = "";
                }
                else if ((setA == "" && setB == setUniversum) ||
                         (setA == setUniversum && setB == ""))
                {
                    resultSymmetricDifference = setUniversum;
                }
                else if ((setA != setUniversum && setA != "") && setB == "")
                {
                    resultSymmetricDifference = setA;
                }
                else if (setA == "" && (setB != setUniversum && setB != ""))
                {
                    resultSymmetricDifference = setB;
                }
                else if ((setA != "" && setA != setUniversum) && setB == setUniversum)
                {
                    resultSymmetricDifference = setUniversum;
                    //============= not A======================
                    for (int i = 0; i < resultSymmetricDifference.Length; i++)
                    {
                        foreach (char cр in setA)
                        {
                            if (cр == resultSymmetricDifference[i])
                            {
                                resultSymmetricDifference = resultSymmetricDifference.Remove(i, 1);
                            }
                        }
                    }
                }
                else if (setA == setUniversum && (setB != "" && setB != setUniversum))
                {
                    resultSymmetricDifference = setUniversum;
                    //============= not B======================
                    for (int i = 0; i < resultSymmetricDifference.Length; i++)
                    {
                        foreach (char cр in setB)
                        {
                            if (cр == resultSymmetricDifference[i])
                            {
                                resultSymmetricDifference = resultSymmetricDifference.Remove(i, 1);
                            }
                        }
                    }
                }
                else if (setA == setB)
                {
                    resultSymmetricDifference = "";
                }

                else////////////////////////////////////////////////
                {
                    if (ItsIncludes(setA, setB))// a in b
                    {
                        //============= not A======================
                        resultSymmetricDifference = setB;
                        for (int i = 0; i < resultSymmetricDifference.Length; i++)
                        {
                            foreach (char cр in setA)
                            {
                                if (cр == resultSymmetricDifference[i])
                                {
                                    resultSymmetricDifference = resultSymmetricDifference.Remove(i, 1);
                                }
                            }
                        }

                    }
                    else if (ItsIncludes(setB, setA))// b in a
                    {
                        //============= not B======================
                        resultSymmetricDifference = setA;
                        for (int i = 0; i < resultSymmetricDifference.Length; i++)
                        {
                            foreach (char cр in setB)
                            {
                                if (cр == resultSymmetricDifference[i])
                                {
                                    resultSymmetricDifference = resultSymmetricDifference.Remove(i, 1);
                                }
                            }
                        }
                    }
                    else
                    {
                        //пересекаются, не пересекаются
                        if (IsCross(setA, setB))
                        {
                            //пересекаются
                            // (a+b)\(a*b)
                            string defAB = CalculateUnion(setA, setB);
                            string defBA = CalculateIntersection(setB, setA);
                            resultSymmetricDifference = CalculateDifference(defAB, defBA);
                            // или
                            
                            //(a\b)+(b\a)
                        }
                        else
                        {
                            //не пересекаются
                            resultSymmetricDifference = "";
                        }
                    }
                }
            }

            return resultSymmetricDifference;
        }

        private string CalculateComplement(string setB, string setA)
        {
            string resultComplement = "";

            setA = new string(setA.Distinct().ToArray());
            setB = new string(setB.Distinct().ToArray());
            setA = string.Concat(setA.OrderBy(x => x).ToArray());
            setB = string.Concat(setB.OrderBy(x => x).ToArray());

            if (checkData(setA, setB))
            {
                resultComplement = "";
            }
            else
            {
                if (setB == "" && setA == "")
                {
                    resultComplement = "";
                }
                else if (setB == setUniversum && setA == setUniversum)
                {
                    resultComplement = "";
                }
                else if (setB == setUniversum && setA == "")//u 0
                {
                    resultComplement = setUniversum;
                }
                else if (setB == "" && setA == setUniversum)//0 u
                {
                    resultComplement = "";
                }
                else if (setB == "" && (setA != setUniversum && setA != ""))//
                {
                    resultComplement = "";
                }
                else if (setA == "" && (setB != setUniversum && setB != ""))
                {
                    resultComplement = setB;
                }
                else if (setB == setUniversum && (setA != "" && setA != setUniversum))//u a
                {
                    resultComplement = setUniversum;
                    //============= not A======================
                    for (int i = 0; i < resultComplement.Length; i++)
                    {
                        foreach (char cр in setA)
                        {
                            if (cр == resultComplement[i])
                            {
                                resultComplement = resultComplement.Remove(i, 1);
                            }
                        }
                    }
                }
                else if ((setB != "" && setB != setUniversum) && setA == setUniversum)//b u
                {
                    resultComplement = "";
                }
                else if (setB == setA)
                {
                    resultComplement = "";
                }
                else//принадлежит, не принадлежит//////////////////////////////////
                {
                    if (ItsIncludes(setB, setA))// b in a
                    {
                        resultComplement = "";
                    }
                    else if (ItsIncludes(setA, setB))// a in b
                    {
                        //============= notA ======================
                        resultComplement = setB;
                        for (int i = 0; i < resultComplement.Length; i++)
                        {
                            foreach (char cр in setA)
                            {
                                if (cр == resultComplement[i])
                                {
                                    resultComplement = resultComplement.Remove(i, 1);
                                }
                            }
                        }
                    }
                    else if (IsCross(setA, setB))
                    {
                        //============= notB ======================
                        resultComplement = setA;
                        for (int i = 0; i < resultComplement.Length; i++)
                        {
                            foreach (char cр in setB)
                            {
                                if (cр == resultComplement[i])
                                {
                                    resultComplement = resultComplement.Remove(i, 1);
                                }
                            }
                        }
                    }
                    else
                    {
                        resultComplement = "";
                    }
                }
            }

            return resultComplement;
        }

        private void CalculateTask()
        {
            //// (A\B)∩(C\B\A)
            string sA = initDataByRendom(30);
            string sB = initDataByRendom(60);
            string sC = initDataByRendom(120);

            string difAB = CalculateDifference(sA, sB);      //  A - B = x
            string unAB = CalculateUnion(sA, sB);            //  A + B
            string difCBA = CalculateDifference(sC, unAB);   //  C - (B-A) = y

            string result = CalculateIntersection(difAB, difCBA);   // (x) * (y)

            ShowResultTask(sA, sB, sC, difAB, unAB, difCBA, result, "∩");
        }

        private void CalculateTaskV2()
        {
            //// (A\B)U(C\B\A)
            string sA = initDataByRendom(30);
            string sB = initDataByRendom(60);
            string sC = initDataByRendom(120);

            string difAB = CalculateDifference(sA, sB);      //  A - B = x
            string unAB = CalculateUnion(sA, sB);            //  A + B
            string difCBA = CalculateDifference(sC, unAB);   //  C - (B+A) = y

            string result = CalculateUnion(difAB, difCBA);   // (x) + (y)

            ShowResultTask(sA, sB, sC, difAB, unAB, difCBA, result, "U");
        }

        //////////////// show result ////////////////////////////////

        private void ShowPicture(int i)
        {
            pictureBox1.Visible = false;// объединение
            pictureBox2.Visible = false;// пересечение
            pictureBox3.Visible = false;// разнось
            pictureBox4.Visible = false;// сим.разность
            pictureBox5.Visible = false;// дополнение А до В
            pictureBox6.Visible = false;// по заданию
            pictureBox7.Visible = false;// empty
            pictureBox8.Visible = false;// set U
            pictureBox9.Visible = false;// error
            pictureBox10.Visible = false;// set A
            pictureBox11.Visible = false;// not cross
            pictureBox12.Visible = false;// set B
            pictureBox13.Visible = false;// not B
            pictureBox14.Visible = false;// not A
            pictureBox15.Visible = false;// B in A
            pictureBox16.Visible = false;// B in A

            switch (i)
            {
                case 1: pictureBox1.Visible = true; break;
                case 2: pictureBox2.Visible = true; break;
                case 3: pictureBox3.Visible = true; break;
                case 4: pictureBox4.Visible = true; break;
                case 5: pictureBox5.Visible = true; break;
                case 6: pictureBox6.Visible = true; break;
                case 7: pictureBox7.Visible = true; break;
                case 8: pictureBox8.Visible = true; break;
                case 9: pictureBox9.Visible = true; break;
                case 10: pictureBox10.Visible = true; break;
                case 11: pictureBox11.Visible = true; break;
                case 12: pictureBox12.Visible = true; break;
                case 13: pictureBox13.Visible = true; break;
                case 14: pictureBox14.Visible = true; break;
                case 15: pictureBox15.Visible = true; break;
                case 16: pictureBox16.Visible = true; break;
            }
        }

        private void ShowResultUnion(string setA, string setB, string result)
        {
            label3.Visible = false;
            label5.Visible = true;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            textBox3.Visible = false;
            
            setA = new string(setA.Distinct().ToArray());
            setB = new string(setB.Distinct().ToArray());
            setA = string.Concat(setA.OrderBy(x => x).ToArray());
            setB = string.Concat(setB.OrderBy(x => x).ToArray());

            if (checkData(setA, setB))
            {
                label5.Text = "Ошибка данных !";
                ShowPicture(9);
            }
            else
            {
                if (setA == "" && setB == "")//0 0
                {
                    label5.Text = Convert.ToString('\u00D8') + 
                                  " ∪ " + 
                                  Convert.ToString('\u00D8')+ 
                                  " = " +
                                  Convert.ToString('\u00D8');
                    ShowPicture(7);
                }
                else if (setA == setUniversum && setB == setUniversum)// u u
                {
                    label5.Text = "U ∪ U = U";
                    ShowPicture(8);
                }
                else if (setA == "" && setB == setUniversum)// 0 u
                {
                    label5.Text = Convert.ToString('\u00D8') + " ∪ U = U";
                    ShowPicture(8);
                }
                else if (setA == setUniversum && setB == "")//u 0
                {
                    label5.Text = "U ∪ " + Convert.ToString('\u00D8') + " = U";
                    ShowPicture(8);
                }
                else if ((setA != setUniversum && setA != "") && setB == "")//a 0
                {
                     label5.Text = "A ∪ " + Convert.ToString('\u00D8') + " = A";
                        ShowPicture(10);
                }
                else if (setA == "" && (setB != setUniversum && setB != ""))//0 b
                {
                    label5.Text = Convert.ToString('\u00D8') + " ∪ B = B";
                    ShowPicture(12);
                }
                else if (((setA != "" && setA != setUniversum) && setB == setUniversum))//a u
                {
                     label5.Text = "A ∪ U = U";
                     ShowPicture(8);
                }
                else if ((setA == setUniversum && (setB != "" && setB != setUniversum)))//u b
                {
                    label5.Text = "U ∪ B = U";
                    ShowPicture(8);
                }
                else if(setA == setB)
                {
                    label5.Text = "A ∪ B = A (множества равны)";
                    ShowPicture(10);
                }
                else
                {
                    if (ItsIncludes(setB, setA))// b in a
                    {
                        label5.Text = "A ∪ B = А (В принадлежит А)";
                        ShowPicture(10);
                    }
                    else if (ItsIncludes(setA, setB))// a in b
                    {
                        label5.Text = "A ∪ B = В (А принадлежит В)";
                        ShowPicture(12);
                    }
                    else if (IsCross(setA, setB))
                    {
                        label5.Text = "A ∪ B";
                        ShowPicture(1);
                        }
                    else
                    {
                        label5.Text = "A ∪ B = AB (множества не пересекаются)";
                        ShowPicture(11);
                    }
                }
            }

            textBox1.Text = setA;
            textBox2.Text = setB;
            textBox4.Text = result;
        }

        private void ShowResultIntersection(string setA, string setB, string result)
        {
            label3.Visible = false;
            label5.Visible = true;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            textBox3.Visible = false;

            setA = new string(setA.Distinct().ToArray());
            setB = new string(setB.Distinct().ToArray());
            setA = string.Concat(setA.OrderBy(x => x).ToArray());
            setB = string.Concat(setB.OrderBy(x => x).ToArray());

            if (checkData(setA, setB))
            {
                label5.Text = "Ошибка данных !";
                ShowPicture(9);
            }
            else
            {
                if (setA == "" && setB == "")//  0 0
                {
                    label5.Text = Convert.ToString('\u00D8') + 
                                  " ∩ " + 
                                  Convert.ToString('\u00D8') +
                                  " = " +
                                  Convert.ToString('\u00D8');
                    ShowPicture(7);
                }
                else if (setA == setUniversum && setB == setUniversum)// u u
                {
                    label5.Text = "U ∩ U = U";
                    ShowPicture(8);
                }
                else if (setA == "" && setB == setUniversum)//0 u
                {
                    label5.Text = Convert.ToString('\u00D8') + 
                                  " ∩ U = " + Convert.ToString('\u00D8');
                    ShowPicture(7);
                }
                else if (setA == setUniversum && setB == "")//u 0
                {
                    label5.Text = "U ∩ " + Convert.ToString('\u00D8') + 
                                  " = " + Convert.ToString('\u00D8');
                        ShowPicture(7);
                }
                else if ((setA != setUniversum && setA != "") && setB == "")//a 0
                {
                    label5.Text = "A ∩ " + Convert.ToString('\u00D8') + 
                                  " = " + Convert.ToString('\u00D8');
                    ShowPicture(7);
                }
                else if (setA == "" && (setB != setUniversum && setB != ""))//0 b
                {
                    label5.Text = Convert.ToString('\u00D8') + " ∩ B = " + 
                                  Convert.ToString('\u00D8');
                        ShowPicture(7);
                }
                else if ((setA != "" && setA != setUniversum) && setB == setUniversum)//a u
                {
                   label5.Text = "A ∩ U = A";
                    ShowPicture(10);
                }
                else if (setA == setUniversum && (setB != "" && setB != setUniversum))//u b
                {
                    label5.Text = "U ∩ B = B";
                        ShowPicture(12);
                }
                else if (setA == setB)// a a
                {
                    label5.Text = "A ∩ A = A (множества равны)";
                    ShowPicture(10);
                }
                else// ab
                {
                    if (ItsIncludes(setB, setA))// b in a
                    {
                        label5.Text = "A ∩ B = B (В принадлежит А)";
                        ShowPicture(15);
                    }
                    else if (ItsIncludes(setA, setB))// a in b
                    {
                        label5.Text = "A ∩ B = А (А принадлежит В)";
                        ShowPicture(5);
                    }
                    else if (IsCross(setA, setB))
                    {
                        label5.Text = "A ∩ B";
                        ShowPicture(2);
                    }
                    else
                    {
                        label5.Text = "A ∩ B = " + Convert.ToString('\u00D8') +
                                      " (множества не пересекаются)";
                        ShowPicture(7);
                    }
                }
            }

            textBox1.Text = setA;
            textBox2.Text = setB;
            textBox4.Text = result;
        }

        private void ShowResultDifference(string setA, string setB, string result)
        {
            label3.Visible = false;
            label5.Visible = true;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            textBox3.Visible = false;

            setA = new string(setA.Distinct().ToArray());
            setB = new string(setB.Distinct().ToArray());
            setA = string.Concat(setA.OrderBy(x => x).ToArray());
            setB = string.Concat(setB.OrderBy(x => x).ToArray());

            if (checkData(setA, setB))
            {
                label5.Text = "Ошибка данных !";
                ShowPicture(9);
            }
            else
            {
                if (setA == "" && setB == "")//  0 0
                {
                        label5.Text = Convert.ToString('\u00D8') + 
                                      " \\ " + 
                                      Convert.ToString('\u00D8')+
                                      " = " +
                                      Convert.ToString('\u00D8');
                        ShowPicture(7);
                }
                else if (setA == setUniversum && setB == setUniversum)// u u
                {
                    label5.Text = "U \\ U = " + Convert.ToString('\u00D8');
                        ShowPicture(7);
                }
                else if (setA == "" && setB == setUniversum)//0 u
                {
                    label5.Text = Convert.ToString('\u00D8') + " \\ U = " + 
                                  Convert.ToString('\u00D8');
                        ShowPicture(7);
                }
                else if (setA == setUniversum && setB == "")//u 0
                {
                        label5.Text = "U \\ " + Convert.ToString('\u00D8') + " = U";
                        ShowPicture(8);
                }
                else if ((setA != setUniversum && setA != "") && setB == "")//a 0
                {
                    label5.Text = "A \\ " + Convert.ToString('\u00D8')+ " = A";
                    ShowPicture(10);
                }
                else if (setA == "" && (setB != setUniversum && setB != ""))//0 b
                {
                    label5.Text = Convert.ToString('\u00D8') + " \\ B = " + 
                                  Convert.ToString('\u00D8');
                    ShowPicture(7);
                }
                else if ((setA != "" && setA != setUniversum) && setB == setUniversum)//a u
                {
                    label5.Text = "A \\ U = " + Convert.ToString('\u00D8');
                    ShowPicture(7);
                }
                else if (setA == setUniversum && (setB != "" && setB != setUniversum))//u b
                {
                    label5.Text = "U \\ B = notB";
                    ShowPicture(13);
                }
                else if (setA == setB)// a a
                {
                    label5.Text = "A \\ B = " + Convert.ToString('\u00D8') + 
                                  " (множества равны)";
                    ShowPicture(7);
                }
                else
                {
                    if (ItsIncludes(setB, setA))// b in a
                    {
                        label5.Text = "A \\ B = notB   (В принадлежит А)";
                        ShowPicture(15);
                    }
                    else if (ItsIncludes(setA, setB))// a in b
                    {
                        label5.Text = "A \\ B = " + Convert.ToString('\u00D8') + 
                                      " (А принадлежит В)";
                        ShowPicture(7);
                    }
                    else if (IsCross(setA, setB))
                    {
                        label5.Text = "A \\ B";
                        ShowPicture(3);
                    }
                    else
                    {
                        label5.Text = "A \\ B = " + Convert.ToString('\u00D8') + 
                                      " (множества не пересекаются)";
                        ShowPicture(7);
                    }                   
                }
            }

            textBox1.Text = setA;
            textBox2.Text = setB;
            textBox4.Text = result;
        }

        private void ShowResultSymmetricDifference(string setA, string setB, string result)
        {
            label3.Visible = false;
            label5.Visible = true;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            textBox3.Visible = false;

            setA = new string(setA.Distinct().ToArray());
            setB = new string(setB.Distinct().ToArray());
            setA = string.Concat(setA.OrderBy(x => x).ToArray());
            setB = string.Concat(setB.OrderBy(x => x).ToArray());

            if (checkData(setA, setB))
            {
                label5.Text = "Ошибка данных !";
                ShowPicture(9);
            }
            else
            {
                if (setA == "" && setB == "")//  0 0
                {
                    label5.Text = Convert.ToString('\u00D8') + 
                                  " △ " + 
                                  Convert.ToString('\u00D8') +
                                  " = " +
                                   Convert.ToString('\u00D8');
                        ShowPicture(7);
                }
                else if (setA == setUniversum && setB == setUniversum)// u u
                {
                    label5.Text = "U △ U = " + Convert.ToString('\u00D8');
                        ShowPicture(7);
                }
                else if (setA == "" && setB == setUniversum)//0 u
                {
                        label5.Text = Convert.ToString('\u00D8') + " △ U = U";
                        ShowPicture(8);
                }
                else if (setA == setUniversum && setB == "")//u 0
                {
                        label5.Text = "U △ " + Convert.ToString('\u00D8') + " = U";
                        ShowPicture(8);
                }
                else if ((setA != setUniversum && setA != "") && setB == "")//a 0
                {
                        label5.Text = "A △ " + Convert.ToString('\u00D8') + " = A";
                        ShowPicture(10);
                }
                else if (setA == "" && (setB != setUniversum && setB != ""))//0 b
                {
                        label5.Text = Convert.ToString('\u00D8') + " △ B = B";
                        ShowPicture(12);
                }
                else if ((setA != "" && setA != setUniversum) && setB == setUniversum)//a u
                {
                     label5.Text = "A △ U = notA";
                        ShowPicture(14);
                }
                else if (setA == setUniversum && (setB != "" && setB != setUniversum))//u b
                {
                    label5.Text = "U △ B = notB";
                        ShowPicture(13);
                }
                else if (setA == setB)// a a
                {
                    label5.Text = "A △ A = " + Convert.ToString('\u00D8') + 
                                  " (множества равны)";
                    ShowPicture(7);
                }
                else// ab
                {
                    if (ItsIncludes(setB, setA))// b in a
                    {
                        label5.Text = "A △ B = notB (В принадлежит А)";
                        ShowPicture(13);
                    }
                    else if (ItsIncludes(setA, setB))// a in b
                    {
                        label5.Text = "A △ B = notA (А принадлежит В)";
                        ShowPicture(14);
                    }
                    else if (IsCross(setA, setB))
                    {
                        label5.Text = "A △ B";
                        ShowPicture(4);
                    }
                    else
                    {
                        label5.Text = "A △ B = " + Convert.ToString('\u00D8') + 
                                      " (множества не пересекаются)";
                        ShowPicture(7);
                    }   
                }
            }

            textBox1.Text = setA;
            textBox2.Text = setB;
            textBox4.Text = result;
        }

        private void ShowResultComplement(string setA, string setB, string result)
        {
            label3.Visible = false;
            label5.Visible = true;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            textBox3.Visible = false;

            setA = new string(setA.Distinct().ToArray());
            setB = new string(setB.Distinct().ToArray());
            setA = string.Concat(setA.OrderBy(x => x).ToArray());
            setB = string.Concat(setB.OrderBy(x => x).ToArray());

            if (checkData(setA, setB))
            {
                label5.Text = "Ошибка данных !";
                ShowPicture(9);
            }
            else
            {
                if (setB == "" && setA == "")//  0 0
                {
                    label5.Text = Convert.ToString('\u00D8') + 
                                 " \\\\ " +
                                Convert.ToString('\u00D8') +
                                " = " +
                                Convert.ToString('\u00D8');
                    ShowPicture(7);
                }
                else if (setB == setUniversum && setA == setUniversum)// u u
                {
                    label5.Text = "U \\\\ U = " + Convert.ToString('\u00D8');
                    ShowPicture(7);
                }
                else if (setB == setUniversum && setA == "")//u 0
                {
                    label5.Text = "U \\\\ " + Convert.ToString('\u00D8') + " = U";
                    ShowPicture(8);
                }
                else if (setB == "" && setA == setUniversum)//0 u
                {
                    label5.Text = Convert.ToString('\u00D8') + " \\\\ U = " + 
                                  Convert.ToString('\u00D8');
                    ShowPicture(7);
                }
                else if (setB == "" && (setA != setUniversum && setA != ""))//0 a
                {
                    label5.Text = Convert.ToString('\u00D8') + " \\\\ A = " + 
                                  Convert.ToString('\u00D8');
                    ShowPicture(7);
                }
                else if ((setB != setUniversum && setB != "") && setA == "")//b 0
                {
                    label5.Text = "B \\\\ " + Convert.ToString('\u00D8') + " = B";
                    ShowPicture(12);
                }
                else if (setB == setUniversum && (setA != "" && setA != setUniversum))//u a
                {
                    label5.Text = "U \\\\ A = notA";
                    ShowPicture(14);
                }
                else if ((setB != "" && setB != setUniversum) && setA == setUniversum)//b u
                {
                    label5.Text = "B \\\\ U = " + Convert.ToString('\u00D8');
                    ShowPicture(7);
                }
                else if (setB == setA)// b b
                {
                    label5.Text = "B \\\\ B = " + Convert.ToString('\u00D8') +
                                  " (множества равны)"; ;
                    ShowPicture(7);
                }
                else// ab
                {
                    if (ItsIncludes(setB, setA))// b in a
                    {
                        label5.Text = "B \\\\ A = " + Convert.ToString('\u00D8') + 
                                      " (В принадлежит А)";
                        ShowPicture(7);
                    }
                    else if (ItsIncludes(setA, setB))// a in b
                    {
                        label5.Text = "B \\\\ A = notA (А принадлежит В)";
                        ShowPicture(14);
                    }
                    else if (IsCross(setA, setB))
                    {
                        label5.Text = "B \\\\ A";
                        ShowPicture(5);
                    }
                    else
                    {
                        label5.Text = "B \\\\ A = " + Convert.ToString('\u00D8') + 
                                      " (множества не пересекаются)";
                        ShowPicture(7);
                    }
                }
            }

            textBox1.Text = setA;
            textBox2.Text = setB;
            textBox4.Text = result;
        }

        private void ShowResultTask(string initialA, string initialB, string initialC,
                                    string dif_ab, string dif_cb, string dif_cba,
                                    string result_task, string operation)
        {
            if (operation == "U")
            {
                pictureBox6.Visible = false;
                pictureBox16.Visible = true;
            }
            else
            {
                pictureBox16.Visible = false;
                pictureBox6.Visible = true;
            }

            label3.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            textBox3.Visible = true;

            textBox1.Text = initialA;
            textBox2.Text = initialB;
            textBox3.Text = initialC;
            textBox4.Text = result_task;

            label5.Text = "(А \\ B) " + operation + " (C \\ B \\ А)";
            label6.Text = "(A - B) : " + dif_ab;
            label7.Text = "(B + A) : " + dif_cb;
            label8.Text = "(C - (B + A)): " + dif_cba;

            DisableEdit();
        }
        
        ////////////////////// buttons ////////////////////////////

        private void btnInputData_Click(object sender, EventArgs e)
        {
            initDataByManual();
        }

        private void btnDataFromFile_Click(object sender, EventArgs e)
        {
            clearData();
            initDataFromFile();
        }

        private void ntbUnion_Click(object sender, EventArgs e)
        {
            string sA = textBox1.Text;
            string sB = textBox2.Text;

            string resUnion = CalculateUnion(sA, sB);
            DisableEdit();
            ShowResultUnion(sA, sB, resUnion);
        }

        private void btnIntersection_Click(object sender, EventArgs e)
        {
            string sA = textBox1.Text;
            string sB = textBox2.Text;

            string resIntersection = CalculateIntersection(sA, sB);
            DisableEdit();
            ShowResultIntersection(sA, sB, resIntersection);
        }

        private void btnDifference_Click(object sender, EventArgs e)
        {
            string sA = textBox1.Text;
            string sB = textBox2.Text;

            string resDifference = CalculateDifference(sA, sB);// (a\b)
            DisableEdit();
            ShowResultDifference(sA, sB, resDifference);
        }

        private void btnSDifference_Click(object sender, EventArgs e)
        {
            string sA = textBox1.Text;
            string sB = textBox2.Text;

            string resSymmetricDifference = CalculateSymmetricDifference(sA, sB);
            DisableEdit();
            ShowResultSymmetricDifference(sA, sB, resSymmetricDifference);
        }

        private void btnComplement_Click(object sender, EventArgs e)
        {
            string sA = textBox1.Text;
            string sB = textBox2.Text;

            string resComplement = CalculateComplement(sB, sA);// (b\a)
            DisableEdit();
            ShowResultComplement(sA, sB, resComplement);
        }

        private void btnByTask_Click(object sender, EventArgs e)
        {
            clearData();
            CalculateTask();
        }

        private void btnByTask_ClickV2(object sender, EventArgs e)
        {
             clearData();
             CalculateTaskV2();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearData();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }        
    }
}
