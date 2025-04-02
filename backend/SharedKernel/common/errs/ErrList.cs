using System.Text;

namespace SharedKernel.common.errs;

public class ErrList
{
    private readonly List<Err> _errList;

    public ErrList() => _errList = [];

    public ErrList(Err err) => _errList = [err];

    public void Add(Err err) => _errList.Add(err);

    public void AddPossibleErr<ErrOrT>(ErrOr<ErrOrT> possibleErr) {
        if (possibleErr.IsErr(out var err)) {
            _errList.Add(err);
        }
    }

    public void AddPossibleErr(ErrOrNothing possibleErr) {
        if (possibleErr.IsErr(out var err)) {
            _errList.Add(err);
        }
    }

    public bool Any() => _errList.Count > 0;
    public int Count() => _errList.Count;

    public Err[] ToArray() => _errList.ToArray();

    public override string ToString() {
        if (_errList.Count == 0) {
            return "No errors";
        }

        if (_errList.Count == 1) {
            return _errList[0].ToString();
        }

        var sb = new StringBuilder();
        for (int i = 0; i < _errList.Count; i++) {
            sb.AppendLine($"Error {i + 1}:\n{_errList[i].ToString()}");
        }

        return sb.ToString();
    }
    public static implicit operator ErrList(Err err) => new(err);
}