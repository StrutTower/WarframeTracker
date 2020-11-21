using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WarframeTrackerTests.TestObjects {
    public enum TestEnum {
        [Description("Description from DescriptionAttribute")]
        WithDescriptionAttribute = 1,
        [Display(Description = "Description from DisplayAttribute")]
        WithDisplayDescription,
        [Description("Description from DescriptionAttribute with both")]
        [Display(Description = "Description from DisplayAttribute with both")]
        WithBoth,
        None
    }
}
