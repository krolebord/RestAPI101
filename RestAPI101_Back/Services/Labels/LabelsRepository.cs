using System;
using RestAPI101_Back.Models;

namespace RestAPI101_Back.Services {
    public class LabelsRepository : ILabelsRepository {
        private readonly RestAppContext context;

        public LabelsRepository(RestAppContext context) {
            this.context = context;
        }

        public bool SaveChanges() => context.SaveChanges() >= 0;

        public void CreateLabel(Label label) {
            if (string.IsNullOrWhiteSpace(label.Name))
                throw new ArgumentNullException(nameof(label), "Name is null or empty");
            if (label.User == null)
                throw new ArgumentNullException(nameof(label), "User is null");

            context.Add(label);
        }

        public void DeleteLabel(Label label) {
            if (label == null)
                throw new ArgumentNullException(nameof(label));

            context.Remove(label);
        }
    }
}