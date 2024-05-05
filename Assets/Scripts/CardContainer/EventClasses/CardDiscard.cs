namespace events {
    public class CardDiscard : CardEvent {
        public CardDiscard(CardWrapper card) : base(card) {
        }
    }
}
