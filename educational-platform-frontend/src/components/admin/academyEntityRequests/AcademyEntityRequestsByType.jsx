import AcademyEntityRequest from "./AcademyEntityRequest";

import classes from "./AcademyEntityRequests.module.css";

const AcademyEntityRequestsByType = ({ heading, requests, resolveRequest }) => {
  return (
    <section className={classes.requests__section}>
      <h2>{heading}</h2>
      <div className={classes.requests}>
        {requests.map((request) => (
          <AcademyEntityRequest
            key={request.id}
            request={request}
            resolveRequest={resolveRequest}
          />
        ))}
      </div>
    </section>
  );
};

export default AcademyEntityRequestsByType;
