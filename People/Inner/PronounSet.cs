namespace izolabella.Util.People.Inner
{
    public class PronounSet
    {
        public PronounSet(string Subjective = "they",
                          string Objective = "them",
                          string PossessiveDeterminer = "their",
                          string PossessivePronoun = "theirs",
                          string ReflexivePronoun = "themselves",
                          bool UseAre = true)
        {
            this.Subjective = Subjective;
            this.Objective = Objective;
            this.PossessiveDeterminer = PossessiveDeterminer;
            this.PossessivePronoun = PossessivePronoun;
            this.ReflexivePronoun = ReflexivePronoun;
            this.UseAre = UseAre;
        }

        /// <summary>
        /// They are cool.
        /// </summary>
        public string Subjective { get; }

        /// <summary>
        /// That house belongs to them.
        /// </summary>
        public string Objective { get; }

        /// <summary>
        /// That is their house.
        /// </summary>
        public string PossessiveDeterminer { get; }

        /// <summary>
        /// The house is theirs.
        /// </summary>
        public string PossessivePronoun { get; }

        /// <summary>
        /// They drove themselves to their house.
        /// </summary>
        public string ReflexivePronoun { get; }

        /// <summary>
        /// Use "are" instead of "is" for this pronoun set.
        /// </summary>
        public bool UseAre { get; }

        /// <summary>
        /// Use "is" instead of "are" for this pronoun set.
        /// </summary>
        public bool UseIs => !this.UseAre;
    }
}
