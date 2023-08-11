import { useEffect, useState } from "react";
import { Link, useParams, useSearchParams } from "react-router-dom";
import classes from "./Confirm.module.css";

const Confirm = () => {
  const params = useParams();
  const [urlSearchParams] = useSearchParams();
  const [confirming, setIsConfirming] = useState(false);
  const [error, setError] = useState();
  const [isSuccess, setIsSuccess] = useState(false);

  useEffect(() => {
    const confirm = async () => {
      try {
        setIsConfirming(true);
        const response = await fetch(
          `${process.env.REACT_APP_BACKEND_URL}user/confirm/${
            params.userId
          }?token=${urlSearchParams.get("token")}`,
          { method: "POST" }
        );

        if (response.status === 400) {
          setError((await response.json()).message);
          setIsSuccess(false);
          setIsConfirming(false);
          return;
        }
        setIsSuccess(true);
      } catch (_) {
        setError("Błąd połączenia z serwerem. Spróbuj ponownie za chwilę");
        setIsSuccess(false);
      }

      setIsConfirming(false);
    };

    confirm();
  }, [params.userId, urlSearchParams]);

  return (
    <div className={classes.confirm}>
      {confirming && "Potwierdzam"}
      {!!error && error}
      {isSuccess && (
        <>
          <p>Pomyślnie potwierdzono konto</p>
          <p className={classes.return}>
            {" "}
            <Link to="/login">Zaloguj</Link>
          </p>
        </>
      )}
      <p className={classes.return}>
        <Link to="/">Kliknij tutaj, aby wrócić na stronę główną</Link>
      </p>
    </div>
  );
};

export default Confirm;
