using System.Collections.Generic;

namespace SpawnerSystem.Shared
{
    public class PickerSelector<T> : ISelector
    {
        public int PickedIndex { get; set; }
        public T PickedItem
        {
            get => List[PickedIndex];
            set => PickedIndex = List.IndexOf(value);
        }

        public IList<T> List { get; private set; }

        public PickerSelector(IList<T> list)
        {
            List = list;
        }

        public int SelectIndex()
        {
            return PickedIndex;
        }
    }
}