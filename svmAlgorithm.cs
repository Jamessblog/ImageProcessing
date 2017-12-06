using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SVM;
using System.IO;
using System.Windows.Forms;

namespace AutoPickingSys
{
    class svmAlgorithm
    {
        public void svmproject(Dictionary<int, Characteristic> Characteristics)
        {
            Dictionary<int, Characteristic> _characteristics = new Dictionary<int, Characteristic>();
            _characteristics = Characteristics;
            Model model;
            RangeTransform range;
            double C;
            double gamma;
            // default values
            Parameter parameters = new Parameter();
            //parameters.SvmType = SvmType.C_SVC;
            //parameters.KernelType = KernelType.RBF;
            //parameters.Degree = 3;
            //parameters.Gamma = 0;
            //parameters.Coefficient0 = 0;
            //parameters.Nu = 0.5;
            //parameters.CacheSize = 40;
            //parameters.C = 1000;
            //parameters.EPS = 1e-3;
            //parameters.P = 0.1;
            //parameters.Shrinking = true;
            //parameters.WeightCount = 0;
            //parameters.WeightLabels = new int[0];
            //parameters.Weights = new double[0];
            //parameters.C = 5;
            //parameters.Gamma = 1;
            string str = null;
            for (int i = 1; i < _characteristics.Count(); i++)
            {
                if (_characteristics[i].IsQualifiedColony == false && _characteristics[i].IsInvalidColony == true)
                {
                    str = str + "0";
                    str = str + " 1:" + _characteristics[i].Area.ToString() + " 2:" + _characteristics[i].MajToMinAxisRatio.ToString() +
                    " 3:" + _characteristics[i].CentreAcerageColor.R.ToString() + " 4:" + _characteristics[i].CentreAcerageColor.G.ToString()
                    + " 5:" + _characteristics[i].CentreAcerageColor.B.ToString() + "\r\n";
                }
                if (_characteristics[i].IsQualifiedColony == true && _characteristics[i].IsInvalidColony == false)
                {
                    str = str + "1";
                    str = str + " 1:" + _characteristics[i].Area.ToString() + " 2:" + _characteristics[i].MajToMinAxisRatio.ToString() +
                    " 3:" + _characteristics[i].CentreAcerageColor.R.ToString() + " 4:" + _characteristics[i].CentreAcerageColor.G.ToString()
                    + " 5:" + _characteristics[i].CentreAcerageColor.B.ToString() + "\r\n";
                }
            }
            if (str != null)
            {
                byte[] array = Encoding.ASCII.GetBytes(str);
                MemoryStream stream = new MemoryStream(array);             //convert stream 2 string  
                Problem train = new Problem();
                train = Problem.Read(stream);
                range = Scaling.DetermineRange(train);
                train = Scaling.Scale(train, range);
                //String outfile001="D:\\parameters.txt";
                ParameterSelection.Grid(train, parameters, @"D:\\parameters.txt", out C, out gamma);
                parameters.C = C;
                parameters.Gamma = gamma;
                model = Training.Train(train, parameters);
                //MessageBox.Show("学习完毕");
                //stream.Dispose();
                stream.Close();
            }
            else
            {
                MessageBox.Show("无学习数据");
                model = null;
                range = null;
            }

            string str1 = null;
            for (int i = 1; i < _characteristics.Count(); i++)
            {
                str1 = str1 + "0";
                str1 = str1 + " 1:" + _characteristics[i].Area.ToString() + " 2:" + _characteristics[i].MajToMinAxisRatio.ToString() +
                    " 3:" + _characteristics[i].CentreAcerageColor.R.ToString() + " 4:" + _characteristics[i].CentreAcerageColor.G.ToString()
                    + " 5:" + _characteristics[i].CentreAcerageColor.B.ToString() + "\r\n";
            }
            if (str1 != null)
            {
                byte[] array = Encoding.ASCII.GetBytes(str1);
                MemoryStream stream = new MemoryStream(array);             //convert stream 2 string  
                Problem pre = new Problem();
                pre = Problem.Read(stream);
                pre = Scaling.Scale(pre, range);
                Prediction.Predict(pre, @"D:\result.txt", model, false);
                MessageBox.Show("筛选完毕");
                //stream.Dispose();
                stream.Close();
            }
            else
            {
                MessageBox.Show("无筛选数据");
            }
            //svm_problem prob = new svm_problem();
            //prob.l = point_list.Count;
            //prob.y = new double[prob.l];
            //            if(param.svm_type == svm_parameter.EPSILON_SVR ||
            //    param.svm_type == svm_parameter.NU_SVR)
            //{
            //    if(param.gamma == 0) param.gamma = 1;
            //    prob.x = new svm_node[prob.l][];
            //    for(int i=0;i<prob.l;i++)
            //    {
            //        point p = (point)point_list[i];					
            //        prob.x[i][0] = new svm_node();
            //        prob.x[i][0].index = 1;
            //        prob.x[i][0].value_Renamed = p.x;
            //        prob.y[i] = p.y;
            //    }
            //    svm_model model = svm.svm_train(prob, param);
            //    svm_node[] x = new svm_node[1];
            //    x[0] = new svm_node();
            //    x[0].index = 1;
            //    int[] j = new int[XLEN];
            //C = Convert.ToInt16(numericUpDown8.Value);
            //gamma = Convert.ToInt16(numericUpDown9.Value);
            //StudyAlgorithm study = new StudyAlgorithm();
            //study.GetModel(AllColony, C, gamma, out model, out range);
            //ScreenAlgorithm screenAlgorithm = new ScreenAlgorithm();
            //screenAlgorithm.ScreenTheColony(CharacteristicsValue, model, range);
 
        }
            
    }
}
