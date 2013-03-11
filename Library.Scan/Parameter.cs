namespace Library.Scan
{
    /// <summary>
    /// BasicDataAccessLayer ve DBObject nesnelerinde kullanýlan parametrelerin sýnýfýdýr.
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// Parametrenin adýný belirtir.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Parametrenin deðerini belirtir.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Parametrenin adý ve deðeri ile birlikte oluþturulmasýný saðlar
        /// </summary>
        /// <param name="name">Parametre adý</param>
        /// <param name="value">Parametre deðeri</param>
        public Parameter(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}