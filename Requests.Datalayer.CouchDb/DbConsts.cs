namespace Cmas.DataLayers.CouchDb.Requests
{
    /// <summary>
    /// Константы БД
    /// </summary>
    internal class DbConsts
    {
        /// <summary>
        /// Имя БД
        /// </summary>
        public const string DbName = "requests";    //FIXME: перенести в конфиг

        /// <summary>
        /// Строка подключения к БД
        /// </summary>
        public const string DbConnectionString = "http://cmas-backend:backend967@cm-ylng-msk-03:5984";    //FIXME: перенести в конфиг

        /// <summary>
        /// Имя дизайн документа
        /// </summary>
        public const string DesignDocumentName = "requests";

        /// <summary>
        /// Имя представления всех документов
        /// </summary>
        public const string AllDocsViewName = "all";

        /// <summary>
        /// Имя представления документов, сгрупированных по ID договора
        /// </summary>
        public const string ByContractDocsViewName = "byContract";

        /// <summary>
        /// Имя представления документов, сгрупированных по ID табеля
        /// </summary>
        public const string ByTimeSheetDocsViewName = "byTimeSheet";
    }
}
