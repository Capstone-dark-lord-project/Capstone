namespace events {
    public class CardEvent {
        public readonly CardWrapper cardWrapper;

        public CardEvent(CardWrapper cardWrapper) {
            this.cardWrapper = cardWrapper;
        }
    }
}
