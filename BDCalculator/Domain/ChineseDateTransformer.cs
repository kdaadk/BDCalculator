using System;
using BDCalculator.Domain.MonthBound;

namespace BDCalculator.Domain
{
    public class ChineseDateTransformer
    {
        private JanuaryBound januaryBound;
        private FebruaryBound februaryBound;
        private MarchBound marchBound;
        private AprilBound aprilBound;
        private MayBound mayBound;
        private JuneBound juneBound;
        private JulyBound julyBound;
        private AugustBound augustBound;
        private SeptemberBound septemberBound;
        private OctoberBound octoberBound;
        private NovemberBound novemberBound;
        private DecemberBound decemberBound;
        
        public ChineseDateTransformer()
        {
            januaryBound = new JanuaryBound();
            februaryBound = new FebruaryBound();
            marchBound = new MarchBound();
            aprilBound = new AprilBound();
            mayBound = new MayBound();
            juneBound = new JuneBound();
            julyBound = new JulyBound();
            augustBound = new AugustBound();
            septemberBound = new SeptemberBound();
            octoberBound = new OctoberBound();
            novemberBound = new NovemberBound();
            decemberBound = new DecemberBound();
        }
        
        public DateTime Transform(DateTime source)
        {
            if (source.Month == januaryBound.Index)
                return Transform(source, januaryBound, 1);
            
            if (source.Month == februaryBound.Index)
                return Transform(source, februaryBound);
            
            if (source.Month == marchBound.Index)
                return Transform(source, marchBound);
            
            if (source.Month == aprilBound.Index)
                return Transform(source, aprilBound);
            
            if (source.Month == mayBound.Index)
                return Transform(source, mayBound);
            
            if (source.Month == juneBound.Index)
                return Transform(source, juneBound);
            
            if (source.Month == julyBound.Index)
                return Transform(source, julyBound);
            
            if (source.Month == augustBound.Index)
                return Transform(source, augustBound);
            
            if (source.Month == septemberBound.Index)
                return Transform(source, septemberBound);
            
            if (source.Month == octoberBound.Index)
                return Transform(source, octoberBound);
            
            if (source.Month == novemberBound.Index)
                return Transform(source, novemberBound);
            
            if (source.Month == decemberBound.Index)
                return Transform(source, decemberBound);
            
            throw new ArgumentException();
        }

        private DateTime Transform(DateTime source, IMonthBound monthBound, int decrementYear = 0)
        {
            var bound = monthBound.BoundDates[source.Year];
            return source.Day < bound
                ? new DateTime(source.Year - decrementYear, source.Month - 1, source.Day)
                : new DateTime(source.Year - decrementYear, source.Month, source.Day);
        }
    }
}