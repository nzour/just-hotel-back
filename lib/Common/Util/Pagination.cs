namespace Common.Util
{
    public class Pagination
    {
        public int? Limit { get; set; }
        public int? Offset { get; set; }

        public Pagination()
        {
            Limit = null;
            Offset = null;
        }

        public Pagination(int? limit = null, int? offset = null)
        {
            Limit = limit;
            Offset = offset;
        }
    }
}