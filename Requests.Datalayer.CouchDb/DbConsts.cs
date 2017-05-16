namespace Cmas.DataLayers.CouchDb.Requests
{
    /// <summary>
    /// Константы БД
    /// </summary>
    internal class DbConsts
    {
        /// <summary>
        /// Имя сущности
        /// </summary>
        public const string ServiceName = "requests";    //FIXME: перенести в конфиг

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
