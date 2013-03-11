namespace Library.Scan
{
    /// <summary>
    /// BasicDataAccessLayer ve DBObject nesnelerinde kullan�lan parametrelerin s�n�f�d�r.
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// Parametrenin ad�n� belirtir.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Parametrenin de�erini belirtir.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Parametrenin ad� ve de�eri ile birlikte olu�turulmas�n� sa�lar
        /// </summary>
        /// <param name="name">Parametre ad�</param>
        /// <param name="value">Parametre de�eri</param>
        public Parameter(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}