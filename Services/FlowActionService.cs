namespace SURI.Challenge.API.Services;

public class FlowActionService<T>
{
    public static object GenerateFlowActionCapture(string title, string body, string buttonTitle, string sectionTitle, List<T> items, Func<T, object> mapItem)
        => new
        {
            Property = 100,
            AlwaysCapture = true,
            VariableType = 2,
            VariableName = typeof(T).Name.ToLower(),
            Delay = 0,
            Type = 7,
            VariableTypeList = new
            {
                Title = title,
                Body = body,
                ButtonTitle = buttonTitle,
                Sections = new[]
                {
                    new
                    {
                        Title = sectionTitle,
                        Options = items.Select(mapItem).ToList()
                    }
                }
            }
        };

    public static object GenerateFlowActionSendMedia(string mediaUrl)
        => new
        {
            MediaType = 1,
            Url = mediaUrl,
            FileName = "Boleto",
            Caption = "",
            Delay = 0,
            Type = 1
        };
}
