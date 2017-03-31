using System;
using System.Collections.Generic;

namespace Cmas.DataLayers.CouchDb.Requests.Dtos
{
    public class RequestDto
    {
        /// <summary>
        /// Уникальный внутренний идентификатор
        /// </summary>
        public String _id;

        /// <summary>
        ///
        /// </summary>
        public String _rev;

        /// <summary>
        /// Идентификатор договора
        /// </summary>
        public string ContractId;

        /// <summary>
        /// Дата и время создания
        /// </summary>
        public DateTime CreatedAt;

        /// <summary>
        /// Дата и время обновления
        /// </summary>
        public DateTime UpdatedAt;

        /// <summary>
        /// Выбранные НЗ по заявке
        /// </summary>
        public IList<string> CallOffOrderIds;

        /// <summary>
        /// Статус заявки
        /// </summary>
        public int RequestStatus;
    }

}