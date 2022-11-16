using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHoney_Flow.Model
{
    class TrainSettingModel
    {

        public TrainSettingModel()
        {
            
        }
        public void SetParam(bool FeatureWise = false, bool SampleWise = false, bool FeatureStdNorm = false, bool SampleStdNorm = false, bool ZCAWhitening = false, bool HorizontalFlip = false, bool VerticalFlip = false)
        {
            Feature_wise = FeatureWise;
            Sample_wise = SampleWise;
            Feature_std_norm = FeatureStdNorm;
            Sample_std_norm = SampleStdNorm;
            ZCA_whitening = ZCAWhitening;
            Horizontal_Flip = HorizontalFlip;
            Vertical_Flip = VerticalFlip;
        }

        public bool Feature_wise = false;
        public bool Sample_wise = false;
        public bool Feature_std_norm = false;
        public bool Sample_std_norm = false;
        public bool ZCA_whitening = false;
        public bool Horizontal_Flip = false;
        public bool Vertical_Flip = false;

    }
}
