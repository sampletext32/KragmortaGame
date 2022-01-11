namespace KragmortaApp.FileDatas
{
    public class PushedStateFileData
    {
        public long? Pusher { get; set; }
        public long? Victim { get; set; }
        public bool ShouldReturnMoveToPusher { get; set; }
    }
}