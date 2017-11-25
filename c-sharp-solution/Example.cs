class Reader {
  public IList Process(StreamReader input) {
    IList result = new ArrayList();
    string line;
    while ((line = input.ReadLine()) != null)
      ProcessLine(line, result);
    return result;
  }

  private void ProcessLine(string line, IList result) {
    if (isBlank(line)) return;
    if (isComment(line)) return;
    string typeCode = GetTypeCode(line);
    IReaderStrategy strategy = (IReaderStrategy)_strategies[typeCode];
    if (null == strategy) 
      throw new Exception("Unable to find strategy");
    result.Add(strategy.Process(line));
  }
  private static bool isComment(string line) {
    return line[0] == '#';
  }
  private static bool isBlank(string line) {
    return line == "";
  }
  private string GetTypeCode(string line) {
    return line.Substring(0,4);
  }
  IDictionary _strategies = new Hashtable();
  public void AddStrategy(IReaderStrategy arg) {
    _strategies[arg.Code] = arg;
  }
}

class ReaderStrategy {
  private string _code;
  private Type _target;
  private IList extractors = new ArrayList();
  public ReaderStrategy(string code, Type target) {
    _code = code;
    this._target = target;
  }
  public string Code {
    get { return _code; }
  }

  public void AddFieldExtractor(int begin, int end, string target) {
    if (!targetPropertyNames().Contains(target)) 
      throw new NoFieldInTargetException(target, _target.FullName);
    extractors.Add(new FieldExtractor(begin, end, target));
  }
  private IList targetPropertyNames() {
    IList result = new ArrayList();
    foreach (PropertyInfo p in _target.GetProperties())
      result.Add(p.Name);
    return result;
  }
  public object Process(string line) {
    object result = Activator.CreateInstance(_target);
    foreach (FieldExtractor ex in extractors)
      ex.extractField(line, result);
    return result;
  }
}

class FieldExtractor {
  private int _begin, _end;
  private string _targetPropertyName;
  public FieldExtractor(int begin, int end, string target) {
    _begin = begin;
    _end = end;
    _targetPropertyName = target;
  }
  public void extractField(string line, object targetObject) {
    string value = line.Substring(_begin, _end - _begin + 1);
    setValue(targetObject, value);
  }
  private void setValue(object targetObject, string value) {
    PropertyInfo prop = targetObject.GetType().GetProperty(_targetPropertyName);
    prop.SetValue(targetObject, value, null);
  }
}

class Main {
  public void Configure(Reader target) {
    target.AddStrategy(ConfigureServiceCall());
    target.AddStrategy(ConfigureUsage());
  }
  private ReaderStrategy ConfigureServiceCall() {
    ReaderStrategy result = new ReaderStrategy("SVCL", typeof (ServiceCall));
    result.AddFieldExtractor(4, 18, "CustomerName");
    result.AddFieldExtractor(19, 23, "CustomerID");
    result.AddFieldExtractor(24, 27, "CallTypeCode");
    result.AddFieldExtractor(28, 35, "DateOfCallString");
    return result;
  }
  private ReaderStrategy ConfigureUsage() {
    ReaderStrategy result = new ReaderStrategy("USGE", typeof (Usage));
    result.AddFieldExtractor(4, 8, "CustomerID");
    result.AddFieldExtractor(9, 22, "CustomerName");
    result.AddFieldExtractor(30, 30, "Cycle");
    result.AddFieldExtractor(31, 36, "ReadDate");
    return result;
  }
}
