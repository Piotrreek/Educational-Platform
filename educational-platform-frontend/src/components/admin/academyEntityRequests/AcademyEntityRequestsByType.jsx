import AcademyEntityRequest from "./AcademyEntityRequest";

import classes from "./AcademyEntityRequests.module.css";

const AcademyEntityRequestsByType = ({ heading, requests }) => {
  return (
    <section className={classes.requests__section}>
      <h2>{heading}</h2>
      <div className={classes.requests}>
        {requests.map((request) => (
          <AcademyEntityRequest key={request.id} request={request} />
        ))}
      </div>
    </section>
  );
};

export default AcademyEntityRequestsByType;
