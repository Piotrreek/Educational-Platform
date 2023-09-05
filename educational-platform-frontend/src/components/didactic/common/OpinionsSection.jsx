import { useState } from "react";
import useAuth from "../../../hooks/useAuth";
import useInput from "../../../hooks/useInput";

import Button from "../../ui/Button";

import { notEmpty } from "../../../utils/validators";
import { createOpinion, getOpinions } from "../../../api/opinionsApi";

import classes from "../didacticMaterial/Material.module.css";

const OpinionsSection = ({
  opinionList,
  noOpinionsText,
  endpoint,
  contentId,
}) => {
  const [opinions, setOpinions] = useState(opinionList);
  const { ctx } = useAuth();
  const [submitting, setSubmitting] = useState(false);
  const [isSuccess, setIsSuccess] = useState(false);
  const { value, isValid, onBlur, onChange, error, hasError, reset } =
    useInput(notEmpty);

  const onSubmit = async (e) => {
    e.preventDefault();
    setSubmitting(true);
    await createOpinion(endpoint, contentId, { opinion: value });
    setOpinions(await getOpinions(endpoint, contentId));

    reset();

    setIsSuccess(true);
    setTimeout(() => {
      setIsSuccess(false);
    }, 2000);
    setSubmitting(false);
  };

  const isLoggedIn = ctx.claims.isLoggedIn;

  return (
    <>
      <section className="content__section content__opinions-section">
        <h2>Opinie</h2>
        {!!opinions.length ? (
          <>
            {opinions.map((opinion, id) => (
              <div className="content__opinions-section__opinion" key={id}>
                <p className="content__opinions-section__date">
                  Data dodania:
                  <span>
                    {new Date(opinion.createdOn).toLocaleDateString("pl-PL")}
                  </span>
                </p>
                <p className="content__opinions-section__author">
                  Autor:
                  <span>{opinion.author}</span>
                </p>
                <p>{opinion.opinion}</p>
              </div>
            ))}
          </>
        ) : (
          <>{noOpinionsText}</>
        )}
      </section>

      {isLoggedIn && (
        <section className={`${classes["new-opinion"]} content__section`}>
          <h2>Dodaj nową opinię</h2>
          <form onSubmit={onSubmit}>
            <textarea
              name="opinion"
              onChange={onChange}
              onBlur={onBlur}
              value={value}
            ></textarea>
            <span style={{ color: "coral" }} className={classes.error}>
              {hasError && error}
            </span>
            {isSuccess && (
              <span
                style={{ color: "greenyellow" }}
                className={classes.success}
              >
                Pomyślnie dodano opinię
              </span>
            )}
            <Button disabled={!isValid || submitting}>
              {submitting ? "Dodaję" : "Dodaj"}
            </Button>
          </form>
        </section>
      )}
    </>
  );
};

export default OpinionsSection;
