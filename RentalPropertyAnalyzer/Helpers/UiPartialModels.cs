using Microsoft.AspNetCore.Html;

namespace RentalPropertyAnalyzer.Helpers
{
    public static class UiPartialModels
    {
        public static (string Label, string Value, string? IconClass) StatCard(
            string label,
            string value,
            string? iconClass)
        {
            return (label, value, iconClass);
        }

        public static (string Title, string? Subtitle, IHtmlContent? Actions) PageHeader(
            string title,
            string? subtitle,
            IHtmlContent? actions)
        {
            return (title, subtitle, actions);
        }
    }
}
