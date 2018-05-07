namespace RUTimetable
{
    internal class RemovePracorTutorialKeyWord
    {
        private string subject;

        public RemovePracorTutorialKeyWord(string subject)
        {
            this.subject = subject;
            subject =subject.ToLower();
        }
        public string Process()
        {
            var s = "";
            for(int i=0; i < subject.Length; i++)
            {
                if(i+1<subject.Length && ((subject[i]=='P' && subject[i+1] == 'R')))
                {
                    return s;
                }
                if (i + 1 < subject.Length && ((subject[i] == 'L' && subject[i + 1] == 'E')))
                {
                    return s;
                }
                else if (i + 1 < subject.Length && ((subject[i] == 'T' && subject[i + 1] == 'U')))
                {
                    return s;
                }
                s += subject[i];
            }
            return s;
        }
    }
}