using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace BDCalculator.Models
{
    public static class PentagramModelExtensions
    {
        private static readonly Color[] ColorsOuterValuesIfEqualValues = {Colors.Red, Colors.Blue, Colors.Red, Colors.Blue, Colors.Blue};
        
        public static void SetColors(this PentagramModel model)
        {
            var pentagramInnerValues = new List<PentagramValueModel>
                {model.InnerValues.Heat, model.InnerValues.Humidity, model.InnerValues.Dryness, model.InnerValues.Cold, model.InnerValues.Wind};
            var pentagramOuterValues = new List<PentagramValueModel>
                {model.OuterValues.Heat, model.OuterValues.Humidity, model.OuterValues.Dryness, model.OuterValues.Cold, model.OuterValues.Wind};
            

            for (var i = 0; i < 5; i++)
            {
                if (pentagramInnerValues[i].Value > pentagramOuterValues[i].Value)
                    SetColors(pentagramInnerValues[i], pentagramOuterValues[i], Colors.Blue);
                else if (pentagramInnerValues[i].Value < pentagramOuterValues[i].Value)
                    SetColors(pentagramInnerValues[i], pentagramOuterValues[i], Colors.Red);
                else
                    SetColors(pentagramInnerValues[i], pentagramOuterValues[i], ColorsOuterValuesIfEqualValues[i]);
            }
        }

        public static PentagramValueModel[] GetAllValueModels(this PentagramModel model) =>
            new[]
            {
                model.InnerValues.Heat,
                model.OuterValues.Heat,
                model.OuterValues.Wind,
                model.InnerValues.Wind,
                model.InnerValues.Dryness,
                model.OuterValues.Dryness,
                model.OuterValues.Humidity,
                model.InnerValues.Humidity,
                model.InnerValues.Heat,
                model.OuterValues.Heat,
                model.OuterValues.Cold,
                model.InnerValues.Cold
            };

        public static PentagramValueModel[] GetInnerValueModels(this PentagramModel model) =>
            GetValueModelsBy(model.InnerValues);
        
        public static PentagramValueModel[] GetOuterValueModels(this PentagramModel model) =>
            GetValueModelsBy(model.OuterValues);

        private static PentagramValueModel[] GetValueModelsBy(PentagramValuesModel model) =>
            new[] {model.Heat, model.Humidity, model.Dryness, model.Cold, model.Wind};

        private static void SetColors(PentagramValueModel innerPentagramValueModel, PentagramValueModel outerPentagramValueModel, Color outerColor)
        {
            outerPentagramValueModel.Color = outerColor;
            innerPentagramValueModel.Color = outerColor == Colors.Blue ? Colors.Red : Colors.Blue;
        }
    }
}