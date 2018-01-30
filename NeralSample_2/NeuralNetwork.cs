using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeralSample_2
{
    public class NeuralNetwork
    {
        /*public HiddenWeight hidden_1
        {
            get;
            set;
        }
        public HiddenWeight hidden_2
        {
            get;
            set;
        }
        public OutWeight output
        {
            get;
            set;
        }

        public void testFunc()
        {

        }*/

        public HiddenWeight[] lstHiddenWeights
        {
            get;
            set;
        }
        public OutWeight outputWeight
        {
            get;
            set;
        }
    }
}
