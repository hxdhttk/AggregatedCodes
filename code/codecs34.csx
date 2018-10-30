public class Solution
{
    private class CountSub
    {
        public int Count { get; set; }
        public string Subdomain { get; set; }
    }

    public IList<string> SubdomainVisits (string[] cpdomains)
    {
        return cpdomains.SelectMany (cpdomain =>
            {
                var arr = cpdomain.Split (' ');
                var count = int.Parse (arr[0]);
                var domain = arr[1];
                var subdomains = new List<string> ();
                while (true)
                {
                    subdomains.Add (domain);
                    var index = domain.IndexOf ('.');
                    if (index == -1)
                    {
                        break;
                    }
                    domain = domain.Substring (index + 1);
                }
                var countSubdomainList = new List<CountSub> ();
                foreach (var subdomain in subdomains)
                {
                    countSubdomainList.Add (new CountSub { Count = count, Subdomain = subdomain });
                }
                return countSubdomainList;
            })
            .GroupBy (countsub => countsub.Subdomain)
            .Select (grouping =>
            {
                var subdomain = grouping.Key;
                var totalCount = grouping.Sum (countsub => countsub.Count).ToString ();
                return $"{totalCount} {subdomain}";
            })
            .ToList ();
    }
}