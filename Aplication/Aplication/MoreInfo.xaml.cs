namespace Aplication;

public partial class MoreInfo : ContentPage
{
	public DateTime startDateTime;
    public DateTime endDateTime;
    public MoreInfo()
	{
		InitializeComponent();
		Setup();
	}
	public void Setup()
	{
		DateTime startDateTime = DateTime.Now;
		endDateTime = new DateTime(startDateTime.Year, startDateTime.Month, startDateTime.Day, startDateTime.Hour + 8, startDateTime.Minute, startDateTime.Second);
        //endDateTime.ToString();
        startTime.Text = "Work start " + startDateTime.ToString();
		endTime.Text = "Work end " + endDateTime.ToString();
    }
}