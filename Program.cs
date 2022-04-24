using System.Net;
IEnumerable<IEnumerable<T>> GetVariousListData<T>(IEnumerable<T> Data, int SizeOfData)
{
    if (SizeOfData == 1) return Data.Select(dtValue => new T[] { dtValue });
    return GetVariousListData(Data, SizeOfData - 1).SelectMany(dtValue => Data.Where(dtSameData => !dtValue.Contains(dtSameData)), (dtValue1, dtValue2) => dtValue1.Concat(new T[] { dtValue2 }));
}
string url = "https://raw.githubusercontent.com/himesh-suthar/raocodefestv1.0/main/telephone_grid_problem.txt";
WebClient webClient = new WebClient();
using (Stream stream = webClient.OpenRead(url))
{
    using (StreamReader streamReader = new StreamReader(stream))
    {
        string line;
        while ((line = streamReader.ReadLine()) != null)
        {
            int Count = Convert.ToInt32(line);
            Console.WriteLine("Output for " + line + ":");
            IEnumerable<IEnumerable<int>> Result = GetVariousListData(Enumerable.Range(1, Count), Count);
            List<string> liData = new List<string>();
            foreach (IEnumerable<int> IEnum in Result)
            {
                string temp = "";
                foreach (int Combination in IEnum)
                {
                    temp += Combination;
                }
                liData.Add(temp);
            }
            liData.Reverse();
            List<string> liUsedData = new List<string>();
            while (liData.Count > 0)
            {
                int tempCount = Count;
                string tempData = "[";
                int index = 0;
                while(index < liData.Count)
                {
                    string strData = liData[index++];
                    if (strData.StartsWith(tempCount.ToString()) && !liUsedData.Contains(strData))
                    {
                        tempData += "[";
                        foreach (char c in strData)
                        {
                            tempData += c + ",";
                        }
                        tempData = tempData.Remove(tempData.Length - 1) + "],";
                        liData.Remove(strData);
                        index--;
                        tempCount--;
                    }
                }
                tempData = tempData.Remove(tempData.Length - 1) + "]";
                Console.WriteLine(tempData);
            }
        }
    }
}
