namespace izolabella.Util.People.Inner
{
    public class PronounSet(string Subjective = "they",
                      string Objective = "them",
                      string PossessiveDeterminer = "their",
                      string PossessivePronoun = "theirs",
                      string ReflexivePronoun = "themselves",
                      bool UseAre = true)
    {

        /// <summary>
        /// They are cool.
        /// </summary>
        public string Subjective { get; } = Subjective;

        /// <summary>
        /// That house belongs to them.
        /// </summary>
        public string Objective { get; } = Objective;

        /// <summary>
        /// That is their house.
        /// </summary>
        public string PossessiveDeterminer { get; } = PossessiveDeterminer;

        /// <summary>
        /// The house is theirs.
        /// </summary>
        public string PossessivePronoun { get; } = PossessivePronoun;

        /// <summary>
        /// They drove themselves to their house.
        /// </summary>
        public string ReflexivePronoun { get; } = ReflexivePronoun;

        /// <summary>
        /// Use "are" instead of "is" for this pronoun set.
        /// </summary>
        public bool UseAre { get; } = UseAre;

        /// <summary>
        /// Use "is" instead of "are" for this pronoun set.
        /// </summary>
        public bool UseIs => !this.UseAre;
    }
}