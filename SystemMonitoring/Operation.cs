using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SystemMonitoring
{
    public enum Operation
    {
        getAllGroups,
        getAllStudents,
        getAllStatus,
        getAllComent_discip,
        getAllComment_discip,
        getAllDiscip_group,
        getAllDiscip_prepod,
        getAllDiscip_prepod_type_work,
        getAllDisciplines,
        getAllEtap_work,
        getAllHistory_stud,
        getAllLectures,
        getAllVariant_type_work,
        getAllWork_stud,
        getAllPrepods,
        getAllType_work,
        // get ids entities
        get_ids_discip,
        get_ids_prepod,
        get_ids_status,
        get_ids_student,
        get_ids_etap_work,
        get_ids_type_work,
        get_ids_groups,
        //  entities
        get_discip,
        get_prepod,
        get_status,
        get_student,
        get_etap_work,
        get_type_work,
        get_groups,
        /// <summary>
        /// Получение истории перемещений студента по группам. Получение массива ID записей
        /// </summary>
        get_ids_hystory_stud,
        /// <summary>
        /// Получение записей из истории перемещений студента по группам.
        /// </summary>
        get_hystory_stud,
        /// <summary>
        /// Получение групп, связанных с дисциплиной.
        /// </summary>
        get_ids_groups_for_dicsip,
        /// <summary>
        /// Получение ID записей соответствий с дисциплинами конкретного преподавателя.
        /// </summary>
        get_ids_discip_prepod,
        /// <summary>
        /// Получение записей соответствий с дисциплинами конкретного преподавателя.
        /// </summary>
        get_discip_prepod,
        /// <summary>
        /// Получение ID записей соответствий с дисциплинами конкретного преподавателя с типами заданий.
        /// </summary>
        get_ids_discip_prepod_type_work,
        /// <summary>
        /// Получение записей соответствий с дисциплинами конкретного преподавателя с типами заданий.
        /// </summary>
        get_discip_prepod_type_work,
        /// <summary>
        /// Получение записи о сдачи конкретного задания конкретным студентом.
        /// </summary>
        get_work_stud
    }
}
