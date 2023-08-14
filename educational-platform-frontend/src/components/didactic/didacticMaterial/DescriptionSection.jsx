const DescriptionSection = ({ description }) => {
  return (
    <section>
      <h2>Opis</h2>
      <p>{!!description ? description : "Ten materiał nie posiada opisu"}</p>
    </section>
  );
};

export default DescriptionSection;
