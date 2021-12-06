namespace DishOrder.Model
{
    public class Table : BaseViewModel
    {
        private int tableId;
        private string tableName;

        public int TableId
        {
            get => tableId;
            set
            {
                if (value == tableId) return;
                tableId = value;
                OnPropertyChanged();
            }
        }

        public string TableName
        {
            get => tableName;
            set
            {
                if (value == tableName) return;
                tableName = value;
                OnPropertyChanged();
            }
        }
    }
}
