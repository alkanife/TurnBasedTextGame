namespace TextGame.utils;

public class Table
{
    public string HorizontalSep;
    public string VerticalSep;
    public string JoinSep;
    public string[] Headers;
    public List<string[]> Rows;
    public bool RightAlign;

    public Table()
    {
        HorizontalSep = "-";
        Rows = new List<string[]>();
        SetShowVerticalLines(false);
    }

    public void SetRightAlign(bool rightAlign) {
        this.RightAlign = rightAlign;
    }

    public void SetShowVerticalLines(bool showVerticalLines) {
        VerticalSep = showVerticalLines ? "|" : "";
        JoinSep = showVerticalLines ? "+" : " ";
    }

    public void SetHeader(string[] headers) {
        Headers = headers;
    }

    public void AddRow(string[] cells) {
        Rows.Add(cells);
    }

    public void Print() {
        var maxWidths = Headers != null ? Headers.Select(x => x.Length).ToArray() : null;

        foreach (var cells in Rows)
        {
            if (maxWidths == null)
                maxWidths = new int[cells.Length];

            if (cells.Length != maxWidths.Length)
                throw new ArgumentException("Number of row-cells and headers should be consistent");

            for (int i = 0; i < cells.Length; i++)
                maxWidths[i] = Math.Max(maxWidths[i], cells[i].Length);
        }

        if (Headers != null) {
            PrintLine(maxWidths);
            PrintRow(Headers, maxWidths);
            PrintLine(maxWidths);
        }

        foreach (var cells in Rows)
            PrintRow(cells, maxWidths);

        if (Headers != null)
            PrintLine(maxWidths);
    }

    private void PrintLine(IReadOnlyList<int> columnWidths) {
        for (var i = 0; i < columnWidths.Count; i++)
        {
            var multiply = columnWidths[i] + VerticalSep.Length + 1;
            var line = "";

            for (var j = 0; j < multiply; j++)
                line += HorizontalSep;
            
            Console.Write(JoinSep + line + (i == columnWidths.Count - 1 ? JoinSep : ""));
        }
        Console.WriteLine();
    }

    private void PrintRow(IReadOnlyList<string> cells, IReadOnlyList<int> maxWidths) {
        for (var i = 0; i < cells.Count; i++) {
            var s = cells[i];
            var verStrTemp = i == cells.Count - 1 ? VerticalSep : "";

            if (RightAlign) {
                Console.Write("{0} " + AnsiColors.Yellow + "{1," + maxWidths[i] + "} " + AnsiColors.Reset + "{2}", VerticalSep, s, verStrTemp);
            } else {
                Console.Write("{0} " + AnsiColors.Yellow + "{1,-" + maxWidths[i] + "} " + AnsiColors.Reset + "{2}", VerticalSep, s, verStrTemp);
            }
        }
        Console.WriteLine();
    }

}