namespace verell.UI
{
    //Только для уникальных ui-элементов
    //Должен находиться в UI-сцене, не создается
    public abstract class UIWidget : BaseUIElement
    {
        public abstract UIWidgetType WidgetType { get; }
    }

    public enum UIWidgetType
    {
        MenuBack = 10,
    }
}